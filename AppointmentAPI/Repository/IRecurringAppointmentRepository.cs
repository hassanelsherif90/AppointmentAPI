using AppointmentAPI.Model;

namespace AppointmentAPI.Repository
{
    public interface IRecurringAppointmentRepository
    {
        Task<RecurringAppointment> GetByIdAsync(string id);
        Task<IEnumerable<RecurringAppointment>> GetAllAsync();
        Task AddAsync(RecurringAppointment appointment);
        Task UpdateAsync(RecurringAppointment appointment);
        Task DeleteAsync(string id);
        Task<IEnumerable<RecurringAppointment>> GetRecurringAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

}
