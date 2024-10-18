

//// 4. إضافة طريقة في وحدة التحكم
//[HttpPost("recurring")]
//public async Task<ActionResult<IEnumerable<Appointment>>> CreateRecurringAppointment(RecurringAppointmentDto dto)
//{
//    try
//    {
//        var appointments = await _appointmentService.CreateRecurringAppointmentsAsync(dto);
//        return Ok(appointments);
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}

//[HttpGet("recurring/{id}")]
//public async Task<ActionResult<IEnumerable<Appointment>>> GetUpcomingRecurringAppointments(int id, [FromQuery] int count = 5)
//{
//    try
//    {
//        var appointments = await _appointmentService.GetUpcomingRecurringAppointmentsAsync(id, count);
//        return Ok(appointments);
//    }
//    catch (NotFoundException)
//    {
//        return NotFound();
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}
