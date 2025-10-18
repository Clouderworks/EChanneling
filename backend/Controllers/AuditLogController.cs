using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/audit-logs")]
    public class AuditLogController : ControllerBase
    {
        // In-memory log store for demo
        private static List<AuditLog> Logs = new List<AuditLog>();

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<AuditLog>> GetAll() => Ok(Logs.OrderByDescending(l => l.Timestamp));

        // For logging from other controllers
        public static void Log(string userId, string username, string action, string entity, string entityId, string details)
        {
            Logs.Add(new AuditLog
            {
                UserId = userId,
                Username = username,
                Action = action,
                Entity = entity,
                EntityId = entityId,
                Details = details
            });
        }
    }
}
