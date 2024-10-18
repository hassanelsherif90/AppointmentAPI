using AppointmentAPI.DTO;
using AppointmentAPI.Model;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var appointments = await _service.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(string id)
        {
            try
            {
                var appointment = await _service.GetAppointmentById(id);
                return Ok(appointment);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(AppointmentDTO appointmentDTO)
        {
            try
            {
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid().ToString(),
                    Service = appointmentDTO.Service,
                    ClientName = appointmentDTO.ClientName,
                    DateTime = appointmentDTO.DateTime,
                    RecurringAppointmentId = null  // Set this only if it's part of a recurring appointment
                };

                var createdAppointment = await _service.CreateAppointment(appointment);
                return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.Id }, createdAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(string id, AppointmentDTO appointmentDTO)
        {
            try
            {
                var existingAppointment = await _service.GetAppointmentById(id);

                existingAppointment.ClientName = appointmentDTO.ClientName;
                existingAppointment.Service = appointmentDTO.Service;
                existingAppointment.DateTime = appointmentDTO.DateTime;

                await _service.UpdateAppointment(id, existingAppointment);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            try
            {
                await _service.DeleteAppointment(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("recurring")]
        public async Task<ActionResult<IEnumerable<Appointment>>> CreateRecurringAppointment(RecurringAppointmentDto dto)
        {
            try
            {
                var appointments = await _service.CreateRecurringAppointmentsAsync(dto);
                return Ok(appointments);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("recurring/{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetUpcomingRecurringAppointments(string id)
        {
            try
            {
                var appointments = await _service.GetUpcomingRecurringAppointmentsAsync(id);
                return Ok(appointments);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
