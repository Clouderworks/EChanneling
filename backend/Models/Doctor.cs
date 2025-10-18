using System;

namespace Backend.Models
{
    public class Doctor
    {
        public Guid DoctorId { get; set; }
        public Guid UserId { get; set; }
        public string? Specialty { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Qualifications { get; set; }
        public string? Department { get; set; }
        public string? ClinicLocation { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}