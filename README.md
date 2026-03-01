# HR Management API

A clean, layered ASP.NET Core Web API for HR management — featuring JWT authentication with refresh tokens and full CRUD operations for employee management.
---

## Project Structure

```
solution/
├── Entities/          # Database models (EF Core entities)
├── DTO/               # Data Transfer Objects (request & response shapes)
├── Data/              # DbContext, interfaces, and repositories
├── Business/          # Service interfaces and business logic
└── API/               # Controllers, extensions, and app entry point
```

---

## Features

-  **JWT Authentication** with access & refresh tokens
-  **Refresh token rotation** with revocation support
-  **Resource-based authorization** (users can only edit their own data)
-  **5-layer architecture** — clean separation of concerns
-  **Rate limiting** (IP-based, fixed window)
-  **Soft delete** for users
-  **Global exception handling**
-  **Swagger UI** with Bearer token support
-  **Auto-migration** on startup

---

