
using AppointmentAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentAPI.Repository.Data.Config
{
    public class RecurringAppointmentConfiguration : IEntityTypeConfiguration<RecurringAppointment>
    {
        public void Configure(EntityTypeBuilder<RecurringAppointment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.RecurrenceType)
                .HasConversion(
                     x => x.ToString(),
                     x => (RecurrenceType)Enum.Parse(typeof(RecurrenceType), x));



            builder.Property(x => x.RecurrenceInterval)
                .HasColumnName("RecurrenceInterval")
                .HasColumnType("int")
                .IsRequired();



            builder.Property(x => x.StartDate)
                .HasColumnName("StartDate")
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnName("EndDate")
                .HasColumnType("DateTime")
                .IsRequired(false);


         

            builder.ToTable("RecurringAppointment");
        }
    }
}
