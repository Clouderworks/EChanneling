using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _service;

        public PatientController(PatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetById(Guid id)
        {
            var patient = _service.GetById(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Patient patient)
        {
            if (patient.PatientId == Guid.Empty)
                patient.PatientId = Guid.NewGuid();
            _service.Create(patient);
            return CreatedAtAction(nameof(GetById), new { id = patient.PatientId }, patient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Patient patient)
        {
            if (id != patient.PatientId) return BadRequest();
            _service.Update(patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}