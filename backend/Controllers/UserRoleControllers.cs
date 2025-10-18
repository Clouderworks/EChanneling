using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<User>> GetAll() => Ok(new List<User>());

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<User> GetById(string id) => Ok(new User { Id = id });

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> Register(User user) => CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Role>> GetAll() => Ok(new List<Role>());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Role> Create(Role role) => CreatedAtAction(nameof(GetAll), new { id = role.Id }, role);
    }
}
