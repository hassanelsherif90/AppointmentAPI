using AppointmentAPI.DTO;
using AppointmentAPI.Model;
using AppointmentAPI.Repository;

namespace AppointmentAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly INotificationService _notificationService;
        private readonly IRecurringAppointmentRepository _recurringAppointmentRepository;

        public AppointmentService(
            IAppointmentRepository repository,
            IRecurringAppointmentRepository recurringAppointmentRepository)
        {
            _repository = repository;
            _recurringAppointmentRepository = recurringAppointmentRepository;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Appointment> GetAppointmentById(string id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            if (appointment == null)
            {
                throw new NotFoundException("Appointment not found");
            }
            return appointment;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            ValidateAppointment(appointment);
            await EnsureTimeSlotAvailable(appointment.DateTime);

            var newAppointment = await _repository.AddAsync(appointment);


            return newAppointment;
        }

        public async Task UpdateAppointment(string id, Appointment appointment)
        {
            var existingAppointment = await GetAppointmentById(id);

            ValidateAppointment(appointment);

            if (existingAppointment.DateTime != appointment.DateTime)
            {
                await EnsureTimeSlotAvailable(appointment.DateTime);
            }

            await _repository.UpdateAsync(appointment);
        }

        public async Task DeleteAppointment(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<DateTime>> GetAvailableTimeSlots(DateTime date)
        {
            var workingHours = new List<DateTime>();
            var startTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            var endTime = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0);

            for (var dt = startTime; dt < endTime; dt = dt.AddMinutes(30))
            {
                if (await IsTimeSlotAvailable(dt))
                {
                    workingHours.Add(dt);
                }
            }

            return workingHours;
        }

        public async Task<List<Appointment>> CreateRecurringAppointmentsAsync(RecurringAppointmentDto dto)
        {
            var recurringAppointment = new RecurringAppointment
            {
                Id = Guid.NewGuid().ToString(),
                RecurrenceType = dto.RecurrenceType,
                RecurrenceInterval = dto.RecurrenceInterval,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            await _recurringAppointmentRepository.AddAsync(recurringAppointment);

            var appointments = new List<Appointment>();
            var currentDate = dto.StartDate;
            var occurrenceCount = 0;

            while ((dto.EndDate == null || currentDate <= dto.EndDate) &&
                   (dto.OccurrenceCount == null || occurrenceCount < dto.OccurrenceCount))
            {
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientName = dto.ClientName,
                    Service = dto.Service,
                    DateTime = currentDate,
                    RecurringAppointmentId = recurringAppointment.Id,
                };

                appointments.Add(await CreateAppointment(appointment));

                currentDate = GetNextOccurrence(currentDate, dto.RecurrenceType, dto.RecurrenceInterval);
                occurrenceCount++;
            }

            return appointments;
        }

        public async Task<List<Appointment>> GetUpcomingRecurringAppointmentsAsync(string recurringAppointmentId)
        {
            var recurringAppointment = await _recurringAppointmentRepository.GetByIdAsync(recurringAppointmentId);
            if (recurringAppointment == null)
            {
                throw new NotFoundException("Recurring appointment not found");
            }

            var appointments = await _repository.GetUpcomingByRecurringIdAsync(recurringAppointmentId);

            while (appointments.Count < 5 && (recurringAppointment.EndDate == null || appointments.Last().DateTime <= recurringAppointment.EndDate))
            {
                var lastAppointment = appointments.Last();
                var nextAppointment = new Appointment
                {
                    ClientName = lastAppointment.ClientName,
                    Service = lastAppointment.Service,
                    DateTime = GetNextOccurrence(lastAppointment.DateTime, recurringAppointment.RecurrenceType, recurringAppointment.RecurrenceInterval),
                    RecurringAppointmentId = recurringAppointmentId
                };

                appointments.Add(await _repository.AddAsync(nextAppointment));
            }

            return appointments;
        }

        private void ValidateAppointment(Appointment appointment)
        {

            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment));
            }
            if (string.IsNullOrWhiteSpace(appointment.ClientName))
            {
                throw new ArgumentException("Client name is required");
            }
            if (string.IsNullOrWhiteSpace(appointment.Service))
            {
                throw new ArgumentException("Service is required");
            }
            if (appointment.DateTime <= DateTime.Now)
            {
                throw new ArgumentException("Appointment date must be in the future");
            }
        }

        private async Task EnsureTimeSlotAvailable(DateTime dateTime)
        {
            if (!await IsTimeSlotAvailable(dateTime))
            {
                throw new InvalidOperationException("The requested time slot is not available");
            }
        }

        private async Task<bool> IsTimeSlotAvailable(DateTime dateTime)
        {
            var existingAppointments = await _repository.GetAllAsync();
            return !existingAppointments.Any(a => a.DateTime == dateTime);
        }

        private DateTime GetNextOccurrence(DateTime current, RecurrenceType type, int interval)
        {
            return type switch
            {
                RecurrenceType.Daily => current.AddDays(interval),
                RecurrenceType.Weekly => current.AddDays(7 * interval),
                RecurrenceType.Monthly => current.AddMonths(interval),
                RecurrenceType.Yearly => current.AddYears(interval),
                _ => current.AddYears(interval)
            };
        }
    }
}
