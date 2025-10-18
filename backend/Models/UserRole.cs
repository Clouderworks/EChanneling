using System;

namespace Backend.Models
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}