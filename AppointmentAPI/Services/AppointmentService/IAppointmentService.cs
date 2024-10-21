using AppointmentAPI.DTO;
using AppointmentAPI.Model;

namespace AppointmentAPI.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointments();

        Task<Appointment> GetAppointmentById(string id);
        Task<Appointment> CreateAppointment(Appointment appointment);

        Task UpdateAppointment(string id, Appointment appointment);

        Task DeleteAppointment(string id);

        Task<List<DateTime>> GetAvailableTimeSlots(DateTime date);

        Task<List<Appointment>> CreateRecurringAppointmentsAsync(RecurringAppointmentDto dto);

        Task<List<Appointment>> GetUpcomingRecurringAppointmentsAsync(string recurringAppointmentId, int count);
    }
}
