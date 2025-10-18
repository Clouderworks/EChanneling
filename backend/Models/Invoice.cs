using System;

namespace Backend.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public Guid PatientId { get; set; }
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}