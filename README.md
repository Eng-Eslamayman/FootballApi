# Football API Project

## Project Overview
Welcome to the Football API project! This API is designed to provide information about football leagues, clubs, and players. The project incorporates the principles of Inversion of Control (IoC) and Dependency Injection (DI) for a modular and maintainable codebase. It also includes features like JWT and Refresh Token for secure authentication and authorization. AutoMapper is utilized for efficient object mapping, and the API exposes three controllers: League, Club, and Player.

## Table of Contents
- [Project Overview](#project-overview)
- [Key Features](#key-features)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Dependencies](#dependencies)

## Key Features

1. **Dependency Injection and IoC Principles:** The project follows the principles of IoC and Dependency Injection, promoting modular and maintainable code by allowing components to be loosely coupled.

2. **JWT and Refresh Token:** JSON Web Tokens (JWT) are employed for secure user authentication and authorization. Refresh Tokens provide a mechanism for obtaining new access tokens without requiring the user to re-enter their credentials.

3. **AutoMapper:** AutoMapper is used to streamline object mapping, making it more efficient and reducing the need for repetitive boilerplate code.

4. **Three Controllers:**
   - **League Controller:** Manages football leagues, providing endpoints for creating, retrieving, updating, and deleting leagues.
   - **Club Controller:** Handles football clubs, offering endpoints for managing club information.
   - **Player Controller:** Manages football players, providing endpoints for player details.

## Getting Started

To get started with the Football API project, follow these steps:

1. Clone or download the project repository to your local machine.

2. Open the project in your preferred development environment.

3. Configure the project settings, including database connection strings, JWT token settings, and other environment-specific configurations.

4. Run the project and access the API endpoints to retrieve information about football leagues, clubs, and players.

## API Endpoints

The project offers the following API endpoints for various functionalities:

- `/api/league`: Endpoints for managing football leagues.
- `/api/club`: Endpoints for managing football clubs.
- `/api/player`: Endpoints for managing football players.

Detailed API documentation and usage examples can be found in the project's API documentation or by accessing the API endpoints directly through a tool like Postman or Swagger.

## Dependencies

The project relies on several dependencies and libraries to function. Some of the key dependencies include:
- AutoMapper
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity
- Microsoft.Extensions.DependencyInjection

Make sure to install and configure these dependencies to ensure the project runs smoothly.
