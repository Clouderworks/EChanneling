using System;

namespace Backend.Models
{
    public class MaoriHealthModel
    {
        public Guid MaoriHealthModelId { get; set; }
        public Guid PatientId { get; set; }
        public string? WhanauSupport { get; set; }
        public string? TraditionalPractices { get; set; }
        public string? SpiritualBeliefs { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}