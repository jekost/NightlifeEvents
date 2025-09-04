## NightlifeEvents - a trial task developed for Softwerk.

### Technologies used:
[![C#](https://img.shields.io/badge/C%23-10-blue.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/) \
[![.NET](https://img.shields.io/badge/.NET-8-brightgreen.svg)](https://dotnet.microsoft.com/) \
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-lightblue.svg)](https://learn.microsoft.com/en-us/aspnet/core/) \
[![xUnit](https://img.shields.io/badge/xUnit-Unit%20Testing-orange.svg)](https://xunit.net/)



The project provides a simple in-memory API for managing nightlife events. You can create, view, and filter events and explore the API with Swagger.

## Setup guide using .NET 8 (SDK 8.0.413)

### Clone & enter the repository
- ```git clone https://github.com/jekost/NightlifeEvents```
- ```cd NightlifeEvents```

### Run the API
- ```dotnet run --project NightlifeEvents.Api```

### Run the tests
- ```dotnet test```

## Accessing the endpoints

### Access Swagger via
- ```http://localhost:5192/swagger/index.html```

| Method | Endpoint           | Description                    |
| ------ | ------------------ | ------------------------------ |
| POST   | `/api/createEvent` | Create a new event             |
| GET    | `/api/events`      | Access all events              |
| GET    | `/api/filter`      | Filter events via city or date |
| GET    | `/api/{id}`        | Access an event via ID         |


## Contact
Jan Erik Köst\
jan.erik.kost@hotmail.com\
(+372) 5304 7697
