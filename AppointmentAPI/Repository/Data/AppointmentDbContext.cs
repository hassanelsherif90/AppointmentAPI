using AppointmentAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository.Data
{
    public class AppointmentDbContext : DbContext
    {

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<RecurringAppointment> RecurringAppointments { get; set; }


        public AppointmentDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentDbContext).Assembly);
        }
    }
}