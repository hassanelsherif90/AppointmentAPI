using AppointmentAPI.Model;

namespace AppointmentAPI.Repository
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment> GetByIdAsync(string id);
        Task<Appointment> AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(string id);
        Task<List<Appointment>> GetUpcomingByRecurringIdAsync(string recurringAppointmentId);
    }
}
