using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedForiegnKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_RecurringAppointment_RecurringAppointmentId",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "RecurringAppointmentId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "RecurringAppointmentId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_RecurringAppointment_RecurringAppointmentId",
                table: "Appointment",
                column: "RecurringAppointmentId",
                principalTable: "RecurringAppointment",
                principalColumn: "Id");
        }
    }
}
