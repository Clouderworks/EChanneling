
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using BCrypt.Net;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {


        private readonly string _connectionString;
        public AuthController(string connectionString)
        {
            _connectionString = connectionString;
        }


        [HttpPost("login")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            try
            {
                User user = null;
                var roles = new List<string>();
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(@"SELECT u.UserId, u.Username, u.PasswordHash, r.Name as RoleName FROM UserAccount u
                        LEFT JOIN UserRole ur ON u.UserId = ur.UserId
                        LEFT JOIN Role r ON ur.RoleId = r.RoleId
                        WHERE u.Username = @username AND u.IsActive = 1", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", req.Username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (user == null)
                                {
                                    user = new User
                                    {
                                        Id = reader["UserId"].ToString(),
                                        Username = reader["Username"].ToString(),
                                        PasswordHash = reader["PasswordHash"].ToString(),
                                        Roles = new List<string>()
                                    };
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("RoleName")))
                                    roles.Add(reader["RoleName"].ToString());
                            }
                        }
                    }
                }
                if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                    return Unauthorized(new { message = "Invalid username or password" });
                user.Roles = roles;
                var token = GenerateJwtToken(user);
                Response.Cookies.Append("jwt_token", token, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(8)
                });
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, new { message = "An unexpected error occurred during login.", detail = ex.Message });
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id)
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_jwt_key_12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
