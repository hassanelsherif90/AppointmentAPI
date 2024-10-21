using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecurringAppointment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecurrenceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecurrenceInterval = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringAppointment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Service = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    DateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RecurringAppointmentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_RecurringAppointment_RecurringAppointmentId",
                        column: x => x.RecurringAppointmentId,
                        principalTable: "RecurringAppointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_RecurringAppointmentId",
                table: "Appointment",
                column: "RecurringAppointmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "RecurringAppointment");
        }
    }
}
