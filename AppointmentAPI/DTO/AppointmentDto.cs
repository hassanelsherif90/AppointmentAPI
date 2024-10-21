namespace AppointmentAPI.DTO
{
    public class AppointmentDTO
    {
        public string ID { get; set; }
        public string ClientName { get; set; }
        public string Service { get; set; }
        public DateTime DateTime { get; set; }
        //public int? OccurrenceCount { get; set; } // عدد مرات التكرار (بديل لـ EndDate)
    }
}
