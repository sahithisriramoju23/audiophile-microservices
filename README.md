# Audiophile Microservices E-Commerce Platform

Audiophile is a modern, cloud-ready e-commerce platform built using a microservices architecture. This project demonstrates advanced software engineering practices and is designed for scalability, maintainability, and extensibility.

## Key Features
- **Microservices Architecture:** Each business domain (Product, Cart, User, etc.) is implemented as an independent .NET 8 service, enabling modular development and deployment.
- **.NET 8 & C# 12:** Leverages the latest .NET and C# features for performance, reliability, and productivity.
- **Entity Framework Core:** Used for data access and migrations, providing a robust ORM layer.
- **PostgreSQL:** Reliable, open-source relational database for persistent storage.
- **CQRS (Command Query Responsibility Segregation):** Clean separation of read and write operations for scalability and maintainability.
- **DDD (Domain-Driven Design):** Codebase is organized around business domains, promoting a rich and expressive domain model.
- **AWS Cognito Integration:** Secure user authentication and authorization, with Lambda triggers for custom workflows (e.g., post-signup confirmation logic).
- **AWS Lambda & Serverless:** Event-driven components and background processing using AWS Lambda, demonstrating serverless patterns.
- **Minimal APIs:** Modern, lightweight HTTP APIs for fast and efficient endpoints.
- **MediatR:** In-process messaging for decoupled command and query handling.
- **Mapster:** Fast object mapping for DTOs and domain models.
- **Docker-Ready:** Services can be containerized for local development or cloud deployment.
- **Extensive Use of Dependency Injection:** Promotes testability and loose coupling.
- **Clean Architecture Principles:** Clear separation of concerns between application, domain, and infrastructure layers.

## Solution Structure
- **Services/**: Contains core microservices (Product.API, Cart.API, User.API) each with their own domain, data, and API layers.
- **Workers/**: Background and event-driven workers, such as Cognito Lambda triggers for user lifecycle events.
- **BuildingBlocks/**: Shared libraries and abstractions for cross-cutting concerns.

## Skills Demonstrated
- Advanced .NET and C# development
- Microservices and distributed systems
- Cloud-native/serverless development (AWS Lambda, Cognito)
- Database design and migrations (PostgreSQL, EF Core)
- Domain-driven and clean architecture
- Secure authentication and authorization
- Automated dependency injection and testing patterns
- API design with minimal APIs and OpenAPI/Swagger

## Getting Started
1. Clone the repository
2. Configure environment variables and connection strings (see `appsettings.json` and UserSecrets)
3. Run database migrations for each service
4. Start services using `dotnet run` or Docker Compose
5. Explore the API endpoints using Swagger UI

---
This project is a showcase of modern .NET engineering and cloud-native design, ideal for demonstrating your skills to potential employers or as a foundation for real-world e-commerce solutions.
