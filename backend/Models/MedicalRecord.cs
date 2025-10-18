using System;

namespace Backend.Models
{
    public class MedicalRecord
    {
        public Guid MedicalRecordId { get; set; }
        public Guid PatientId { get; set; }
        public string? RecordType { get; set; }
        public string? Description { get; set; }
        public DateTime RecordDate { get; set; }
        public string? FilePath { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}