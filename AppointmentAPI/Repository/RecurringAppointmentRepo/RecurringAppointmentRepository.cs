using AppointmentAPI.Model;
using AppointmentAPI.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository.RecurringAppointmentRepo
{
    public class RecurringAppointmentRepository : IRecurringAppointmentRepository
    {
        private readonly AppointmentDbContext _context;

        public RecurringAppointmentRepository(AppointmentDbContext context)
        {
            _context = context;
        }

        public async Task<RecurringAppointment> GetByIdAsync(string id)
        {
            return await _context.RecurringAppointments.FindAsync(id);
        }

        public async Task<IEnumerable<RecurringAppointment>> GetAllAsync()
        {
            return await _context.RecurringAppointments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(RecurringAppointment appointment)
        {
            await _context.RecurringAppointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RecurringAppointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var appointment = await GetByIdAsync(id);
            if (appointment != null)
            {
                _context.RecurringAppointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RecurringAppointment>> GetRecurringAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.RecurringAppointments
                .AsNoTracking()
                .Where(ra => ra.StartDate >= startDate && ra.EndDate <= endDate)
                .ToListAsync();
        }
    }
}
