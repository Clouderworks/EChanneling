using System;

namespace Backend.Models
{
    public class Waitlist
    {
        public Guid WaitlistId { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime RequestedAt { get; set; }
        public string? Status { get; set; }
        public DateTime? NotifiedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}