using AppointmentAPI.Model;

namespace AppointmentAPI.DTO
{
    public class RecurringAppointmentDto
    {
        public string ClientName { get; set; }
        public string Service { get; set; }
        public DateTime StartDate { get; set; } 
        public RecurrenceType RecurrenceType { get; set; }
        public int RecurrenceInterval { get; set; }
        public DateTime? EndDate { get; set; }
        public int? OccurrenceCount { get; set; } // عدد مرات التكرار (بديل لـ EndDate)
    }
}
