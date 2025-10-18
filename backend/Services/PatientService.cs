using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Backend.Models;
using Microsoft.Extensions.Configuration;

namespace Backend.Services
{
    public class PatientService
    {
        private readonly string _connectionString;

        public PatientService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Patient> GetAll()
        {
            var patients = new List<Patient>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM Patients", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(MapPatient(reader));
                    }
                }
            }
            return patients;
        }

        public Patient? GetById(Guid id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM Patients WHERE PatientId = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapPatient(reader);
                }
            }
            return null;
        }

        public void Create(Patient patient)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"INSERT INTO Patients (PatientId, UserId, BloodType, Height, Weight, EmergencyContactName, EmergencyContactPhone, EmergencyContactRelationship, InsuranceProvider, InsurancePolicyNumber, InsuranceExpiryDate, PrimaryCareProviderId, PrimaryFacilityId, ConsentForDataSharing, ConsentForResearch, ConsentForMarketing, TelehealthPreference, AIHealthMonitoringEnabled, WearableDeviceIntegration, PrefersMaoriHealthModel, WhanauInvolvementLevel, TraditionalHealingPreferences, AIRiskScore, RiskFactors, LastRiskAssessment, IsActive, CreatedAt, UpdatedAt) VALUES (@PatientId, @UserId, @BloodType, @Height, @Weight, @EmergencyContactName, @EmergencyContactPhone, @EmergencyContactRelationship, @InsuranceProvider, @InsurancePolicyNumber, @InsuranceExpiryDate, @PrimaryCareProviderId, @PrimaryFacilityId, @ConsentForDataSharing, @ConsentForResearch, @ConsentForMarketing, @TelehealthPreference, @AIHealthMonitoringEnabled, @WearableDeviceIntegration, @PrefersMaoriHealthModel, @WhanauInvolvementLevel, @TraditionalHealingPreferences, @AIRiskScore, @RiskFactors, @LastRiskAssessment, @IsActive, @CreatedAt, @UpdatedAt)", conn))
            {
                AddParameters(cmd, patient);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Patient patient)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"UPDATE Patients SET UserId=@UserId, BloodType=@BloodType, Height=@Height, Weight=@Weight, EmergencyContactName=@EmergencyContactName, EmergencyContactPhone=@EmergencyContactPhone, EmergencyContactRelationship=@EmergencyContactRelationship, InsuranceProvider=@InsuranceProvider, InsurancePolicyNumber=@InsurancePolicyNumber, InsuranceExpiryDate=@InsuranceExpiryDate, PrimaryCareProviderId=@PrimaryCareProviderId, PrimaryFacilityId=@PrimaryFacilityId, ConsentForDataSharing=@ConsentForDataSharing, ConsentForResearch=@ConsentForResearch, ConsentForMarketing=@ConsentForMarketing, TelehealthPreference=@TelehealthPreference, AIHealthMonitoringEnabled=@AIHealthMonitoringEnabled, WearableDeviceIntegration=@WearableDeviceIntegration, PrefersMaoriHealthModel=@PrefersMaoriHealthModel, WhanauInvolvementLevel=@WhanauInvolvementLevel, TraditionalHealingPreferences=@TraditionalHealingPreferences, AIRiskScore=@AIRiskScore, RiskFactors=@RiskFactors, LastRiskAssessment=@LastRiskAssessment, IsActive=@IsActive, CreatedAt=@CreatedAt, UpdatedAt=@UpdatedAt WHERE PatientId=@PatientId", conn))
            {
                AddParameters(cmd, patient);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Patients WHERE PatientId = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static Patient MapPatient(IDataReader reader)
        {
            return new Patient
            {
                PatientId = reader.GetGuid(reader.GetOrdinal("PatientId")),
                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                BloodType = reader["BloodType"] as string,
                Height = reader["Height"] as decimal?,
                Weight = reader["Weight"] as decimal?,
                EmergencyContactName = reader["EmergencyContactName"] as string,
                EmergencyContactPhone = reader["EmergencyContactPhone"] as string,
                EmergencyContactRelationship = reader["EmergencyContactRelationship"] as string,
                InsuranceProvider = reader["InsuranceProvider"] as string,
                InsurancePolicyNumber = reader["InsurancePolicyNumber"] as string,
                InsuranceExpiryDate = reader["InsuranceExpiryDate"] as DateTime?,
                PrimaryCareProviderId = reader["PrimaryCareProviderId"] as Guid?,
                PrimaryFacilityId = reader["PrimaryFacilityId"] as Guid?,
                ConsentForDataSharing = reader["ConsentForDataSharing"] as bool?,
                ConsentForResearch = reader["ConsentForResearch"] as bool?,
                ConsentForMarketing = reader["ConsentForMarketing"] as bool?,
                TelehealthPreference = reader["TelehealthPreference"] as bool?,
                AIHealthMonitoringEnabled = reader["AIHealthMonitoringEnabled"] as bool?,
                WearableDeviceIntegration = reader["WearableDeviceIntegration"] as bool?,
                PrefersMaoriHealthModel = reader["PrefersMaoriHealthModel"] as bool?,
                WhanauInvolvementLevel = reader["WhanauInvolvementLevel"] as string,
                TraditionalHealingPreferences = reader["TraditionalHealingPreferences"] as string,
                AIRiskScore = reader["AIRiskScore"] as decimal?,
                RiskFactors = reader["RiskFactors"] as string,
                LastRiskAssessment = reader["LastRiskAssessment"] as DateTime?,
                IsActive = reader["IsActive"] as bool?,
                CreatedAt = reader["CreatedAt"] as DateTime?,
                UpdatedAt = reader["UpdatedAt"] as DateTime?
            };
        }

        private static void AddParameters(SqlCommand cmd, Patient patient)
        {
            cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
            cmd.Parameters.AddWithValue("@UserId", patient.UserId);
            cmd.Parameters.AddWithValue("@BloodType", (object?)patient.BloodType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Height", (object?)patient.Height ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Weight", (object?)patient.Weight ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmergencyContactName", (object?)patient.EmergencyContactName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmergencyContactPhone", (object?)patient.EmergencyContactPhone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmergencyContactRelationship", (object?)patient.EmergencyContactRelationship ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InsuranceProvider", (object?)patient.InsuranceProvider ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InsurancePolicyNumber", (object?)patient.InsurancePolicyNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InsuranceExpiryDate", (object?)patient.InsuranceExpiryDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PrimaryCareProviderId", (object?)patient.PrimaryCareProviderId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PrimaryFacilityId", (object?)patient.PrimaryFacilityId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ConsentForDataSharing", (object?)patient.ConsentForDataSharing ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ConsentForResearch", (object?)patient.ConsentForResearch ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ConsentForMarketing", (object?)patient.ConsentForMarketing ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TelehealthPreference", (object?)patient.TelehealthPreference ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AIHealthMonitoringEnabled", (object?)patient.AIHealthMonitoringEnabled ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@WearableDeviceIntegration", (object?)patient.WearableDeviceIntegration ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PrefersMaoriHealthModel", (object?)patient.PrefersMaoriHealthModel ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@WhanauInvolvementLevel", (object?)patient.WhanauInvolvementLevel ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TraditionalHealingPreferences", (object?)patient.TraditionalHealingPreferences ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AIRiskScore", (object?)patient.AIRiskScore ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RiskFactors", (object?)patient.RiskFactors ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LastRiskAssessment", (object?)patient.LastRiskAssessment ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", (object?)patient.IsActive ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedAt", (object?)patient.CreatedAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)patient.UpdatedAt ?? DBNull.Value);
        }
    }
}