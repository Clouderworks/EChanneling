using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private static System.Reflection.FieldInfo patientsField = typeof(Backend.Controllers.RegistrationController)
            .GetField("Patients", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        private static List<Patient> Patients => (List<Patient>)patientsField.GetValue(null);

        /// <summary>
        /// Get gender distribution analytics. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet("analytics/gender")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult GetGenderDistribution()
        {
            var stats = new Dictionary<string, int>();
            foreach (var p in Patients)
            {
                if (!string.IsNullOrEmpty(p.Gender))
                {
                    if (!stats.ContainsKey(p.Gender)) stats[p.Gender] = 0;
                    stats[p.Gender]++;
                }
            }
            return Ok(stats);
        }

        /// <summary>
        /// Get age group analytics. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet("analytics/age-groups")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult GetAgeGroups()
        {
            var now = System.DateTime.Now;
            var groups = new[] {
                new { Label = "0-18", Min = 0, Max = 18 },
                new { Label = "19-35", Min = 19, Max = 35 },
                new { Label = "36-60", Min = 36, Max = 60 },
                new { Label = "61+", Min = 61, Max = 200 }
            };
            var result = groups.ToDictionary(g => g.Label, g => 0);
            foreach (var p in Patients)
            {
                if (p.DateOfBirth == default) continue;
                var age = (int)((now - p.DateOfBirth).TotalDays / 365.25);
                foreach (var g in groups)
                {
                    if (age >= g.Min && age <= g.Max)
                    {
                        result[g.Label]++;
                        break;
                    }
                }
            }
            return Ok(result);
        }

        /// <summary>
        /// Get ethnicity counts analytics. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet("analytics/ethnicity")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult GetEthnicityCounts()
        {
            var stats = new Dictionary<string, int>();
            foreach (var p in Patients)
            {
                if (!string.IsNullOrEmpty(p.Ethnicity))
                {
                    if (!stats.ContainsKey(p.Ethnicity)) stats[p.Ethnicity] = 0;
                    stats[p.Ethnicity]++;
                }
            }
            return Ok(stats);
        }

        /// <summary>
        /// Get common conditions analytics. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet("analytics/common-conditions")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult GetCommonConditions()
        {
            var freq = new Dictionary<string, int>();
            foreach (var p in Patients)
            {
                if (p.FamilyHistory != null)
                {
                    foreach (var fh in p.FamilyHistory)
                    {
                        if (!string.IsNullOrEmpty(fh.Condition))
                        {
                            if (!freq.ContainsKey(fh.Condition)) freq[fh.Condition] = 0;
                            freq[fh.Condition]++;
                        }
                    }
                }
            }
            var sorted = freq.OrderByDescending(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);
            return Ok(sorted);
        }

        /// <summary>
        /// Get language stats analytics. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet("analytics/languages")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult GetLanguageStats()
        {
            var stats = new Dictionary<string, int>();
            foreach (var p in Patients)
            {
                if (!string.IsNullOrEmpty(p.Language))
                {
                    if (!stats.ContainsKey(p.Language)) stats[p.Language] = 0;
                    stats[p.Language]++;
                }
            }
            return Ok(stats);
        }

        /// <summary>
        /// Get all patients. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult<IEnumerable<Patient>> GetAll() => Ok(Patients);

        /// <summary>
        /// Get patient by ID. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult<Patient> GetById(string id)
        {
            var patient = Patients.Find(p => p.Id == id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        /// <summary>
        /// Update patient. Requires Admin or Doctor role.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public IActionResult Update(string id, Patient updated)
        {
            var patient = Patients.Find(p => p.Id == id);
            if (patient == null) return NotFound();
            var oldPatient = System.Text.Json.JsonSerializer.Serialize(patient);
            patient.FirstName = updated.FirstName;
            patient.LastName = updated.LastName;
            patient.DateOfBirth = updated.DateOfBirth;
            patient.Gender = updated.Gender;
            patient.Ethnicity = updated.Ethnicity;
            patient.Language = updated.Language;
            patient.Address = updated.Address;
            patient.Phone = updated.Phone;
            patient.Email = updated.Email;
            patient.FamilyHistory = updated.FamilyHistory;
            patient.GeneticRisks = updated.GeneticRisks;
            patient.SocialDeterminants = updated.SocialDeterminants;
            AuditLogController.Log(
                User.FindFirst("id")?.Value,
                User.Identity?.Name ?? "",
                "Update",
                "Patient",
                id,
                $"Old: {oldPatient}\nNew: {System.Text.Json.JsonSerializer.Serialize(updated)}"
            );
            return NoContent();
        }

        /// <summary>
        /// Delete patient. Requires Admin role.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            var patient = Patients.Find(p => p.Id == id);
            if (patient == null) return NotFound();
            Patients.Remove(patient);
            AuditLogController.Log(
                User.FindFirst("id")?.Value,
                User.Identity?.Name ?? "",
                "Delete",
                "Patient",
                id,
                $"Deleted: {System.Text.Json.JsonSerializer.Serialize(patient)}"
            );
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        /// <summary>
        /// Get all doctors. Requires Admin or Doctor role.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<IEnumerable<Doctor>> GetAll() => Ok(new List<Doctor>());

        /// <summary>
        /// Get doctor by ID. Requires Admin or Doctor role.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<Doctor> GetById(string id) => Ok(new Doctor { Id = id });

        /// <summary>
        /// Create doctor. Requires Admin role.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Doctor> Create(Doctor doctor) => CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);

        /// <summary>
        /// Update doctor. Requires Admin role.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(string id, Doctor doctor) => NoContent();

        /// <summary>
        /// Delete doctor. Requires Admin role.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id) => NoContent();
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        /// <summary>
        /// Get all appointments. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult<IEnumerable<Appointment>> GetAll() => Ok(new List<Appointment>());

        /// <summary>
        /// Get appointment by ID. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult<Appointment> GetById(string id) => Ok(new Appointment { Id = id });

        /// <summary>
        /// Create appointment. Requires Admin, Doctor, or Receptionist role.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public ActionResult<Appointment> Create(Appointment appointment) => CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);

        /// <summary>
        /// Update appointment. Requires Admin or Doctor role.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public IActionResult Update(string id, Appointment appointment) => NoContent();

        /// <summary>
        /// Delete appointment. Requires Admin role.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id) => NoContent();
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PrescriptionsController : ControllerBase
    {
        /// <summary>
        /// Get all prescriptions. Requires Admin or Doctor role.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<IEnumerable<Prescription>> GetAll() => Ok(new List<Prescription>());

        /// <summary>
        /// Get prescription by ID. Requires Admin or Doctor role.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<Prescription> GetById(string id) => Ok(new Prescription { Id = id });

        /// <summary>
        /// Create prescription. Requires Admin or Doctor role.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<Prescription> Create(Prescription prescription) => CreatedAtAction(nameof(GetById), new { id = prescription.Id }, prescription);

        /// <summary>
        /// Update prescription. Requires Admin or Doctor role.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public IActionResult Update(string id, Prescription prescription) => NoContent();

        /// <summary>
        /// Delete prescription. Requires Admin role.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id) => NoContent();
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicalRecordsController : ControllerBase
    {
        /// <summary>
        /// Get all medical records. Requires Admin or Doctor role.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<IEnumerable<MedicalRecord>> GetAll() => Ok(new List<MedicalRecord>());

        /// <summary>
        /// Get medical record by ID. Requires Admin or Doctor role.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<MedicalRecord> GetById(string id) => Ok(new MedicalRecord { Id = id });

        /// <summary>
        /// Create medical record. Requires Admin or Doctor role.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult<MedicalRecord> Create(MedicalRecord record) => CreatedAtAction(nameof(GetById), new { id = record.Id }, record);

        /// <summary>
        /// Update medical record. Requires Admin or Doctor role.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public IActionResult Update(string id, MedicalRecord record) => NoContent();

        /// <summary>
        /// Delete medical record. Requires Admin role.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id) => NoContent();
    }
}

