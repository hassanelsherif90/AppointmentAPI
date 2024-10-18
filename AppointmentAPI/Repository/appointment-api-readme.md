# Appointment API

## Overview
Appointment API is a RESTful web service for managing appointments and recurring appointments. It provides functionality to create, read, update, and delete appointments, as well as manage recurring appointments.

## Features
- CRUD operations for appointments
- Create and manage recurring appointments
- Get available time slots
- Notification service integration

## Technologies
- ASP.NET Core
- Entity Framework Core
- C# 8.0+

## Prerequisites
- .NET 6.0 SDK or later
- SQL Server (or another compatible database)

## Getting Started

### Installation
1. Clone the repository:
   ```
   git clone https://github.com/your-username/appointment-api.git
   ```
2. Navigate to the project directory:
   ```
   cd appointment-api
   ```
3. Restore the NuGet packages:
   ```
   dotnet restore
   ```

### Configuration
1. Update the connection string in `appsettings.json` to point to your database.
2. Configure the email settings for the notification service in `appsettings.json`.

### Database Setup
Run the following command to create the database and apply migrations:
```
dotnet ef database update
```

### Running the Application
1. Build and run the application:
   ```
   dotnet run
   ```
2. The API will be available at `https://localhost:5001` by default.

## API Endpoints

### Appointments
- `GET /api/appointments`: Get all appointments
- `GET /api/appointments/{id}`: Get a specific appointment
- `POST /api/appointments`: Create a new appointment
- `PUT /api/appointments/{id}`: Update an existing appointment
- `DELETE /api/appointments/{id}`: Delete an appointment

### Recurring Appointments
- `POST /api/appointments/recurring`: Create a recurring appointment
- `GET /api/appointments/recurring/{id}`: Get upcoming recurring appointments

## Usage Examples

### Creating an Appointment
```http
POST /api/appointments
Content-Type: application/json

{
  "clientName": "John Doe",
  "service": "Haircut",
  "dateTime": "2023-06-01T10:00:00Z"
}
```

### Creating a Recurring Appointment
```http
POST /api/appointments/recurring
Content-Type: application/json

{
  "clientName": "Jane Smith",
  "service": "Massage",
  "startDate": "2023-06-01T14:00:00Z",
  "endDate": "2023-12-31T14:00:00Z",
  "recurrenceType": "Weekly",
  "recurrenceInterval": 1
}
```

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
