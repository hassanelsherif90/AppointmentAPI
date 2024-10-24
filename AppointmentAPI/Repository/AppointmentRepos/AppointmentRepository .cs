using AppointmentAPI.Model;
using AppointmentAPI.Repository.AppointmentRepo;
using AppointmentAPI.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository.AppointmentRepository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDbContext _context;

        public AppointmentRepository(AppointmentDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .AsNoTracking()
                .OrderBy(x => x.DateTime)
                .ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(string id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var appointment = await GetByIdAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Appointment>> GetUpcomingByRecurringIdAsync(string recurringAppointmentId)
        {
            // ReturnAppoinments In the future Comming
            return await _context.Appointments
                .AsNoTracking()
                .Where(a => a.RecurringAppointmentId == recurringAppointmentId && a.DateTime > DateTime.UtcNow)
                .OrderBy(a => a.DateTime)
                .ToListAsync();
        }


        public async Task<IEnumerable<Appointment>> AllRecurringAppointment()
        {
            return _context.Appointments.Include(x=>x.RecurringAppointment);
        }
       
    }
}
