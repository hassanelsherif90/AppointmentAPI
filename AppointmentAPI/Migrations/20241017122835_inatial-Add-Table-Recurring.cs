using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class inatialAddTableRecurring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Service",
                table: "Appointment",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Appointment",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Appointment",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "RecurringAppointmentId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_RecurringAppointmentId",
                table: "Appointment",
                column: "RecurringAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_RecurringAppointment_RecurringAppointmentId",
                table: "Appointment",
                column: "RecurringAppointmentId",
                principalTable: "RecurringAppointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_RecurringAppointment_RecurringAppointmentId",
                table: "Appointment");

            migrationBuilder.DropTable(
                name: "RecurringAppointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_RecurringAppointmentId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "RecurringAppointmentId",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "Service",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");
        }
    }
}
