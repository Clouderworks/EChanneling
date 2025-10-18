using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhiController : ControllerBase
    {
        // Simulated NHI lookup (replace with real integration as needed)
        private static readonly List<NhiRecord> NhiDatabase = new()
        {
            new NhiRecord { NhiNumber = "ABC1234", FirstName = "John", LastName = "Doe", DateOfBirth = new System.DateTime(1980, 1, 1) },
            new NhiRecord { NhiNumber = "XYZ5678", FirstName = "Jane", LastName = "Smith", DateOfBirth = new System.DateTime(1990, 5, 10) }
        };

        [HttpGet("{nhiNumber}")]
        public ActionResult<NhiRecord> Lookup(string nhiNumber)
        {
            var record = NhiDatabase.FirstOrDefault(x => x.NhiNumber == nhiNumber);
            if (record == null) return NotFound();
            return Ok(record);
        }
    }

    public class NhiRecord
    {
        public string NhiNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
    }
}
