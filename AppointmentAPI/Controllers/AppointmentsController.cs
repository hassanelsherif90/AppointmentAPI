using AppointmentAPI.DTO;
using AppointmentAPI.Model;
using AppointmentAPI.Services.AppointmentService;
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
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAllAppointments()
        {
            var appointmentDtoList =  new List<AppointmentDTO>();   
            var appointments = await _service.GetAllAppointments();

            foreach (var appointment in appointments)
            {
                var appointmentDto = new AppointmentDTO
                {
                    ID=appointment.Id,
                    ClientName = appointment.ClientName,
                    Service = appointment.Service,
                    DateTime = appointment.DateTime,
                  };

                appointmentDtoList.Add(appointmentDto); 
            }

            return Ok(appointmentDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(string id)
        {
            try
            {
                var appointment = await _service.GetAppointmentById(id);

                var appointmentDTO = new AppointmentDTO
                {
                    ID = appointment.Id,
                    DateTime = DateTime.Now,
                    Service = appointment.Service,
                    ClientName=appointment.ClientName,  
                };

                return Ok(appointmentDTO);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /*[HttpPost]
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
                    RecurringAppointmentId = null
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
        }*/

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
            catch (Exception ex)
            {
                return NotFound();
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
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpGet("GetAllRecurringAppointments")]
        public async Task<ActionResult<IEnumerable<RecurringAppointmentDto>>> GetAllRecurringAppointments()
        {
            var appointments = await _service.GetAllRecurringAppointments();

            var appointmentDto = new List<RecurringAppointmentDto>();   

            foreach (var appointment in appointments)
            {
                var dto = new RecurringAppointmentDto
                {
                    ClientName = appointment.ClientName,
                    Service = appointment.Service,
                    StartDate= appointment.RecurringAppointment.StartDate,
                    EndDate=appointment.RecurringAppointment.EndDate,   
                    RecurrenceType=appointment.RecurringAppointment.RecurrenceType,
                    RecurrenceInterval= appointment.RecurringAppointment.RecurrenceInterval,
                };

                appointmentDto.Add(dto);
            }

            return appointmentDto;


        }


        [HttpPost("recurring")]
        public async Task<ActionResult<IEnumerable<Appointment>>> CreateRecurringAppointment(RecurringAppointmentDto dto)
        {

            var appointments = await _service.CreateRecurringAppointmentsAsync(dto);

            return Ok(appointments);    

        }

        [HttpGet("recurring/{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetUpcomingRecurringAppointments(string id, int count)
        {
            try
            {
                var appointments = await _service.GetUpcomingRecurringAppointmentsAsync(id, count);

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    
    
    }
}
