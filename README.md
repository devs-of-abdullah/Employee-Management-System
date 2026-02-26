# HR Management API

A clean, layered ASP.NET Core Web API for managing employees, departments, roles, and users — with JWT authentication, refresh tokens, and role-based authorization.

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
-  **Role-based authorization** (`admin`, `superAdmin`)
-  **Resource-based authorization** (users can only edit their own data)
-  **5-layer architecture** — clean separation of concerns
-  **Rate limiting** (IP-based, fixed window)
-  **Soft delete** for users
-  **Many-to-many** Employee ↔ Department and Employee ↔ Role
-  **Global exception handling**
-  **Swagger UI** with Bearer token support
-  **Auto-migration** on startup

---





---

## API Endpoints

### Auth — `/api/auth`

| Method | Endpoint         | Auth     | Description                  |
|--------|------------------|----------|------------------------------|
| POST   | `/login`         | None  | Login with email & password  |
| POST   | `/refresh`       | None  | Refresh access token         |
| POST   | `/logout`        | JWT   | Revoke refresh token         |

### Users — `/api/users`

| Method | Endpoint              | Auth              | Description                |
|--------|-----------------------|-------------------|----------------------------|
| GET    | `/{id}`               |  Owner or Admin | Get user by ID               |
| POST   | `/`                   |  None           | Register a new user          |
| PUT    | `/change-password`    |  JWT            | Change own password          |
| PUT    | `/change-email`       |  JWT            | Change own email             |
| DELETE | `/self`               |  JWT            | Soft delete own account      |
| DELETE | `/{id}`               |  Admin          | Admin soft delete a user     |
| PUT    | `/{id}/role`          |  Admin          | Update a user's role         |

### Employees — `/api/employees`

| Method | Endpoint              | Auth   | Description             |
|--------|-----------------------|--------|-------------------------|
| GET    | `/`                   |  JWT | Get all employees       |
| GET    | `/{id}`               |  JWT | Get employee by ID      |
| POST   | `/`                   |  JWT | Create employee         |
| PUT    | `/{id}`               |  JWT | Update employee         |
| PATCH  | `/{id}/activate`      |  JWT | Activate employee       |
| PATCH  | `/{id}/deactivate`    |  JWT | Deactivate employee     |
| DELETE | `/{id}`               |  JWT | Delete employee         |

###  Departments — `/api/departments`
| Method | Endpoint                        | Auth   | Description                    |
|--------|---------------------------------|--------|--------------------------------|
| GET    | `/`                             |  JWT | Get all departments              |
| GET    | `/{id}`                         |  JWT | Get department by ID             |
| POST   | `/`                             |  JWT | Create department                |
| PUT    | `/{id}/name`                    |  JWT | Update department name           |
| PUT    | `/{id}/description`             |  JWT | Update department description    |
| DELETE | `/{id}`                         |  JWT | Delete department                |
| POST   | `/{id}/employees/{employeeId}`  |  JWT | Assign employee to department    |
| DELETE | `/{id}/employees/{employeeId}`  |  JWT | Remove employee from department  |

### 🎭 Roles — `/api/roles`

| Method | Endpoint                        | Auth   | Description                |
|--------|---------------------------------|--------|----------------------------|
| GET    | `/`                             |  JWT | Get all roles                |
| GET    | `/{id}`                         |  JWT | Get role by ID               |
| POST   | `/`                             |  JWT | Create role                  |
| PUT    | `/{id}/name`                    |  JWT | Update role name             |
| PUT    | `/{id}/description`             |  JWT | Update role description      |
| DELETE | `/{id}`                         |  JWT | Delete role                  |
| POST   | `/{id}/employees/{employeeId}`  |  JWT | Assign employee to role      |
| DELETE | `/{id}/employees/{employeeId}`  |  JWT | Remove employee from role    |

---

## 🗄️ Database Schema

```
UserEntity
├── Id, Email, PasswordHash, Role
├── IsDeleted, CreatedAt, UpdatedAt
└── RefreshTokenHash, RefreshTokenExpiresAt, RefreshTokenRevokedAt

EmployeeEntity
├── Id, FirstName, LastName, Email, PhoneNumber
├── HireDate, Salary, IsActive, CreatedDate
├── EmployeeDepartments (many-to-many → DepartmentEntity)
└── EmployeeRoles       (many-to-many → RoleEntity)

DepartmentEntity
└── Id, Name, Description

RoleEntity
└── Id, Name, Description
```

---

## Authentication Flow

```
1. POST /api/auth/login
   → returns { accessToken, refreshToken }

2. Use accessToken in Authorization header:
   Authorization: Bearer <accessToken>

3. When accessToken expires → POST /api/auth/refresh
   → returns new { accessToken, refreshToken }

4. POST /api/auth/logout
   → revokes the refreshToken
```

## Authorization

Two levels of access control are used:

**Role-based** — via `[Authorize(Roles = "admin,superAdmin")]`

**Resource-based** — via `UserOwnerOrAdminRequirement`:
- Admins can access any user
- Regular users can only access their own data (matched by JWT `NameIdentifier` claim)

---



## NuGet Packages

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" />
<PackageReference Include="BCrypt.Net-Next" />
<PackageReference Include="Swashbuckle.AspNetCore" />
```



## 📝 License

MIT
