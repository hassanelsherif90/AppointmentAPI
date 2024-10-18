namespace AppointmentAPI.Services
{
    public class EmailService : INotificationService
    {
        private readonly ILogger<EmailService> _logger;
        // يمكن إضافة خدمة البريد الإلكتروني هنا

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // تنفيذ إرسال البريد الإلكتروني
            _logger.LogInformation($"تم إرسال بريد إلكتروني إلى {to}");

            await Task.CompletedTask;
        }

        public Task SendSmsAsync(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }
    }
}
