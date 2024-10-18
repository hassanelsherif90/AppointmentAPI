using AppointmentAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentAPI.Repository.Data.Config
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClientName)
            .HasColumnName("ClientName")
            .HasMaxLength(50)
            .HasColumnType("NVARCHAR")
            .IsRequired();

            builder.Property(x => x.Service)
            .HasColumnName("Service")
            .HasMaxLength(50)
            .HasColumnType("NVARCHAR")
            .IsRequired();

            builder.Property(x => x.DateTime)
            .HasColumnName("DateTime")
            .HasColumnType("DateTime")
            .IsRequired();

            builder.HasOne(x => x.RecurringAppointment)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.RecurringAppointmentId)
                .IsRequired();

            builder.ToTable("Appointment");
        }
    }
}
