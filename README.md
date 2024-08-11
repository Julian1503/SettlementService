# SettlementService

SettlementService is an ASP.NET Core Web API project designed to manage bookings. The project includes a set of controllers, services, and middleware to handle various aspects of booking management, including validation and error handling.

## Table of Contents

- [Getting Started](#getting-started)
- [Dependencies](#dependencies)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Usage](#usage)

## Getting Started

To get started with the SettlementService project, follow the instructions below.

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Installation

1. Clone the repository:

`git clone https://github.com/yourusername/SettlementService.git`
cd SettlementService

2. Restore the dependencies:
`dotnet restore`

## Dependencies

The project relies on the following NuGet packages:

- `Microsoft.AspNetCore.Mvc`
- `Microsoft.Extensions.Logging`
- `Moq`
- `NUnit`
- `System.Text.Json`

## Running the Application

To run the application, use the following command:

`dotnet run`

## Testing

To run the tests, use the following command:

`dotnet test`

This will execute all the unit tests in the project.

## Usage

### BookingController

The `BookingController` provides endpoints to manage bookings.

#### Add New Booking

- **Endpoint**: `POST /api/Booking`
- **Request Body**:
`{
    "name": "John Doe",
    "bookingTime": "10:00"
}`

- **Responses**:
    - `{"bookingId": "2da9e788-7d25-4bae-955a-f21511e519ef"}`
    - `{"type": "https://tools.ietf.org/html/rfc9110#section-15.5.10", "title": "Conflict", status": 409, "detail": "Booking at this time is full.", "traceId":"00-1fd0bc52d452ca7526d1acc5c6547dba-d43300fb72b7d33b-00"}`

#### Rules

- You can only add up to 4 bookings in a period of time of 1 hour (booking at 9:00am means the spot is held from 9:00am to 9:59am).

- Name and time are required, name cannot be empty and time has to have a correct format (00:00 to 23:59)

- Bussiness hours are setted by default as 9am-5pm

### Example

To add a new booking, send a `POST` request to `http://localhost:5285/api/Booking` with the following JSON body:
`{ "name": "John Doe", "bookingTime": "10:00" }`
