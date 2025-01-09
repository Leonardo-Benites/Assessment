Project Overview:

This is a .NET 8 project that uses Entity Framework with a code-first approach to generate the database. 
The project follows a clean architecture pattern, separating the application into four main layers: Infrastructure, Domain, Application, and API.

Key features include:

-Employee Endpoints: Implemented CRUD operations (GET, POST, PUT, DELETE) and a GET by ID endpoint.

-Department Endpoint: A GET endpoint to fetch department details.

-Partial Authentication Controller: A basic authentication implementation as technical debt for future enhancement.

-AutoMapper Integration: Used for object-to-object mapping, reducing the need for manual mapping.

-API Response Pattern: A standardized response format for all API endpoints, ensuring consistency across the application.

The project is designed to facilitate scalability and maintainability, 
ensuring a clear separation of concerns and effective dependency management across the different layers.