using System;

namespace Backend.Models
{
    public class Feedback
    {
        public Guid FeedbackId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? Comments { get; set; }
        public int? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}