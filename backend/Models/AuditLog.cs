using System;

namespace Backend.Models
{
    public class AuditLog
    {
        public Guid AuditLogId { get; set; }
        public Guid? UserId { get; set; }
        public string? Action { get; set; }
        public string? Entity { get; set; }
        public Guid? EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
        public string? IPAddress { get; set; }
    }
}