using System;

namespace Backend.Models
{
    public class Permission
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string? Description { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public bool? IsActive { get; set; }
    }
}
