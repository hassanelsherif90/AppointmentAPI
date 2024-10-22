﻿// <auto-generated />
using System;
using AppointmentAPI.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppointmentAPI.Migrations
{
    [DbContext(typeof(AppointmentDbContext))]
    [Migration("20241022062904_ModifiedForiegnKey")]
    partial class ModifiedForiegnKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppointmentAPI.Model.Appointment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("ClientName");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateTime");

                    b.Property<string>("RecurringAppointmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Service");

                    b.HasKey("Id");

                    b.HasIndex("RecurringAppointmentId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("AppointmentAPI.Model.RecurringAppointment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("DateTime")
                        .HasColumnName("EndDate");

                    b.Property<int>("RecurrenceInterval")
                        .HasColumnType("int")
                        .HasColumnName("RecurrenceInterval");

                    b.Property<string>("RecurrenceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("DateTime")
                        .HasColumnName("StartDate");

                    b.HasKey("Id");

                    b.ToTable("RecurringAppointment", (string)null);
                });

            modelBuilder.Entity("AppointmentAPI.Model.Appointment", b =>
                {
                    b.HasOne("AppointmentAPI.Model.RecurringAppointment", "RecurringAppointment")
                        .WithMany("Appointments")
                        .HasForeignKey("RecurringAppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecurringAppointment");
                });

            modelBuilder.Entity("AppointmentAPI.Model.RecurringAppointment", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
