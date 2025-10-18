using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        // In-memory patient store (replace with DB in production)
        private static readonly List<Patient> Patients = new();

    /// <summary>
    /// Register a new patient. Requires Admin or Receptionist role.
    /// </summary>
    [HttpPost]
    [Route("")]
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,Receptionist")]
    public ActionResult<Patient> Register(PatientRegistrationDto registration)
        {
            // Simulate NHI lookup (call NhiController in real integration)
            var validNhi = NhiControllerStatic.Lookup(registration.NhiNumber);
            if (validNhi == null)
            {
                return BadRequest(new { message = "Invalid NHI number." });
            }

            var patient = new Patient
            {
                Id = System.Guid.NewGuid().ToString(),
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                DateOfBirth = registration.DateOfBirth,
                Gender = registration.Gender,
                Ethnicity = registration.Ethnicity,
                Language = registration.Language,
                Address = registration.Address,
                Phone = registration.Phone,
                Email = registration.Email,
                NhiNumber = registration.NhiNumber,
                FamilyHistory = registration.FamilyHistory,
                GeneticRisks = registration.GeneticRisks,
                SocialDeterminants = registration.SocialDeterminants
            };
            Patients.Add(patient);
            return CreatedAtAction("GetById", "Patients", new { id = patient.Id }, patient);
        }
    }

    // Static helper for NHI lookup (simulate service call)
    public static class NhiControllerStatic
    {
        private static readonly List<string> ValidNhis = new() { "ABC1234", "XYZ5678" };
        public static object Lookup(string nhiNumber)
        {
            return ValidNhis.Contains(nhiNumber) ? new { NhiNumber = nhiNumber } : null;
        }
    }

    public class PatientRegistrationDto
    {
        public string NhiNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string Language { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<FamilyHistoryEntry> FamilyHistory { get; set; }
        public List<GeneticRiskEntry> GeneticRisks { get; set; }
        public SocialDeterminants SocialDeterminants { get; set; }
    }
}
