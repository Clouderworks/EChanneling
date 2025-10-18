using System;

namespace Backend.Models
{
    public class Patient
    {
        public Guid PatientId { get; set; }
        public Guid UserId { get; set; }
        public string? BloodType { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public DateTime? InsuranceExpiryDate { get; set; }
        public Guid? PrimaryCareProviderId { get; set; }
        public Guid? PrimaryFacilityId { get; set; }
        public bool? ConsentForDataSharing { get; set; }
        public bool? ConsentForResearch { get; set; }
        public bool? ConsentForMarketing { get; set; }
        public bool? TelehealthPreference { get; set; }
        public bool? AIHealthMonitoringEnabled { get; set; }
        public bool? WearableDeviceIntegration { get; set; }
        public bool? PrefersMaoriHealthModel { get; set; }
        public string? WhanauInvolvementLevel { get; set; }
        public string? TraditionalHealingPreferences { get; set; }
        public decimal? AIRiskScore { get; set; }
        public string? RiskFactors { get; set; }
        public DateTime? LastRiskAssessment { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
