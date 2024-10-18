// 1. تحديث نموذج البيا

using System.Text.Json.Serialization;

namespace AppointmentAPI.Model
{
    public class Appointment
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public string Service { get; set; }
        public DateTime DateTime { get; set; }
        public string? RecurringAppointmentId { get; set; }

        [JsonIgnore]
        public RecurringAppointment RecurringAppointment { get; set; }
    }
}
