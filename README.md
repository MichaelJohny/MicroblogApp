# Microblogging Application

This repository contains a robust microblogging application built using **.NET Core 8**. The application is designed with scalability, performance, and maintainability in mind, leveraging modern architectural patterns and technologies.

## Features

- **User Authentication & Authorization**: Powered by ASP.NET Identity with JWT for secure API authentication.
- **Microblogging Functionality**: Users can post microblogs, supporting various media attachments.
- **Optimized Caching**: Uses Redis for high-performance caching.
- **Image Processing**: Resizes images using SixLabors.ImageSharp before storing them in Azure Blob Storage.
- **Scalable and Maintainable Architecture**: Implements Clean Architecture with CQRS and the Mediator pattern using MediatR.
- **Database Management**: Utilizes PostgreSQL with Entity Framework Core (Code-First Migrations).
- **Unit Testing**: Ensures reliability and correctness with xUnit test cases.

---

## Technologies and Tools

### Backend Technologies
- **ASP.NET Core 8**: Provides the framework for building the API.
- **Clean Architecture**: Ensures a separation of concerns and promotes modularity.
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations for better performance and scalability.
- **MediatR**: Implements the Mediator pattern to handle requests and notifications.
- **Entity Framework Core**: Used as the Object-Relational Mapper (ORM) for PostgreSQL.
- **Redis**: Provides caching to improve performance and reduce database load.
- **ASP.NET Identity**: Manages user authentication and authorization.
- **JWT (JSON Web Tokens)**: Secures API endpoints.

### Database
- **PostgreSQL**: Serves as the primary database for storing application data.
- **Code-First Migrations**: Ensures database schema evolves seamlessly with the codebase.

### File Management
- **Azure Blob Storage**: Stores uploaded files, such as images and other media.
- **SixLabors.ImageSharp**: Processes and resizes images before uploading to storage.

### Testing
- **xUnit**: Provides unit testing for application components, ensuring high code quality.

---

## Getting Started

### Prerequisites
1. Install [.NET SDK 8](https://dotnet.microsoft.com/download).
2. Set up PostgreSQL and Redis on your local machine or in the cloud.
3. Configure an Azure Blob Storage account for file uploads.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/MichaelJohny/MicroblogApp.git
   cd microblogging-app
2. run docker compose
    you will find file dock-compose file run docker compose up to get the db - redis - postgresl instance with configurations used at code    

## Project Structure

The project is structured following *Clean Architecture* principles, promoting separation of concerns and modularity.

```plaintext
├── API
│   ├── Controllers           # Handles HTTP requests and responses
│   ├── Middlewares           # Custom middleware for handling cross-cutting concerns
│   └── Startup.cs            # Configures services and middleware
│
├── Application
│   ├── Commands              # Write operations (CQRS)
│   ├── Queries               # Read operations (CQRS)
│   ├── Validators            # FluentValidation for input validation
│   └── Interfaces            # Abstractions used across the application
│
├── Domain
│   ├── Entities              # Core domain models
│   └── Enums                 # Enumerations used in the domain
│
├── Infrastructure
│   ├── Data                  # Data access using Entity Framework Core
│   ├── Caching               # Redis caching implementations
│   ├── Authentication        # ASP.NET Identity and JWT setup
│   └── FileStorage           # Azure Blob Storage integration
│
├── Tests
│   ├── UnitTests             # Unit tests using xUnit
│
├── README.md                 # Project documentation
└── appsettings.json          # Configuration settings
