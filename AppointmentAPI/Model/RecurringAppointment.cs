// 1. تحديث نموذج البيانات


namespace AppointmentAPI.Model
{
    public class RecurringAppointment
    {
        public string Id { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public int RecurrenceInterval { get; set; } // كم مرة يتكرر (كل يوم، كل أسبوعين، إلخ)
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Appointment> Appointments { get; set; } // One To Many
    }
}
