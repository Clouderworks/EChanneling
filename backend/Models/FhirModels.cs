using System;
using System.Collections.Generic;

namespace Backend.Models
{
    // FHIR-compliant Patient model (simplified)
    public class Patient
    {
        public string Id { get; set; }
        public string NhiNumber { get; set; } // NZ Health Index
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string Language { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<MedicalRecord> MedicalRecords { get; set; }

        // Family history, genetic, and social determinants
        public List<FamilyHistoryEntry> FamilyHistory { get; set; }
        public List<GeneticRiskEntry> GeneticRisks { get; set; }
        public SocialDeterminants SocialDeterminants { get; set; }
    }

    public class FamilyHistoryEntry
    {
        public string Relation { get; set; }
        public string Condition { get; set; }
        public string Notes { get; set; }
    }

    public class GeneticRiskEntry
    {
        public string Gene { get; set; }
        public string RiskLevel { get; set; }
        public string Notes { get; set; }
    }

    public class SocialDeterminants
    {
        public string Occupation { get; set; }
        public string Education { get; set; }
        public string Housing { get; set; }
        public string IncomeLevel { get; set; }
        public string MaritalStatus { get; set; }
        public string Notes { get; set; }
    }

    // FHIR-compliant Doctor model (simplified)
    public class Doctor
    {
        public string Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    // FHIR-compliant Appointment model (simplified)
    public class Appointment
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    // FHIR-compliant Prescription model (simplified)
    public class Prescription
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime DatePrescribed { get; set; }
        public List<string> Medications { get; set; }
        public string Notes { get; set; }
    }

    // FHIR-compliant MedicalRecord model (simplified)
    public class MedicalRecord
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}
