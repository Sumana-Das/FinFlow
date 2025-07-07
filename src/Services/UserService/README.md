# 🔐 UserService – FinFlow Microservice
The UserService is the authentication and identity management module for the FinFlow platform—a cloud-native financial workflow system designed with scalable microservices. This service handles secure user registration, login, and JWT-based authentication using ASP.NET Core.

### 📦 Features
✅ User registration and secure password storage (via BCrypt)

✅ Role-based authentication using JWT (default roles: User)

✅ Login flow that issues time-bound JWT tokens

✅ In-memory user store (can be upgraded to persistent DB later)

✅ Swagger UI for testing and exploration

✅ Designed using SOLID principles, clean layering, and testability

✅ Userd `IOptions<T>` instead of `IConfiguration` to get the appconfigurations to avoid tightly coupling hardcoded values

### 🧠 Tech Stack
| Layer              | Tooling                                                             |
|--------------------|----------------------------------------------------------------------|
| **Framework**       | ASP.NET Core 8.0 Web API                                             |
| **Auth**            | JWT (`Microsoft.AspNetCore.Authentication.JwtBearer`)               |
| **Password Security** | BCrypt (`BCrypt.Net-Next`)                                         |
| **DI**              | ASP.NET Core built-in container                                     |
| **Config Management** | `IOptions<T>` for strongly typed JWT settings                     |
| **Documentation**   | Swagger (`Swashbuckle.AspNetCore`)                                  |


### 🗃️ Project Structure
<pre lang="markdown">
UserService/
├── Controllers/        # AuthController (exposes /register and /login)
├── Models/             # Domain model + DTOs for register/login
├── Services/           # IUserService, ITokenService and implementations
├── Helpers/            # Strongly typed JwtSettings bound from config
├── Extensions/         # DI extension for AddJwtAuth
├── appsettings.json    # JWT secret, issuer, audience, expiry
├── Program.cs          # Entry point with middleware setup
└── UserService.csproj
</pre>

### 🔧 How It Works
Passwords are hashed with BCrypt and stored in a thread-safe in-memory store.

AuthController handles POST /register and POST /login endpoints.

Upon successful login, a JWT token is returned containing claims:

sub (username)

role (User or Admin)

The token is signed and validated using symmetric key encryption (HMAC SHA256).

Authentication and DI setup is centralized in ServiceCollectionExtensions.

### 🔐 JWT Flow Demo

` # POST /api/auth/register`
<pre lang="markdown">
{
  "username": "<yourname>", 
  "password": "<yourpassword>"
} </pre>

` # POST /api/auth/login`
<pre lang="markdown">
{
  "username": "<yourname>", 
  "password": "<yourpassword>"
} </pre>

Returns:

<pre lang="markdown">
{ "token": "eyJhbGciOi..." }</pre>

Use this token in future requests:

<pre lang="markdown"> Authorization: Bearer eyJhbGciOi...</pre>
### 🛡️ Extensible Concepts You’ve Practiced
| Concept               | Where Applied                                                                 |
|------------------------|------------------------------------------------------------------------------|
| **OOP Principles**      | Encapsulation, abstraction, dependency inversion                            |
| **Design Patterns**     | Interface segregation (`ITokenService`, `IUserService`), extension method for service wiring |
| **Auth Best Practices** | Claims-based identity, JWT with expiry, password hashing                    |
| **.NET Core Internals** | Model binding, middleware pipeline, options pattern                         |
| **Clean Architecture**  | Clear service boundaries, DTOs vs models separation, config isolation        |

