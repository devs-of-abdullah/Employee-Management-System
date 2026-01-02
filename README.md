# Employee Management System

**Overview**
- This Project is a **Employe Management System APIs**
- built with ASP.NET Core following clean, layered architecture.
- It simulates CRUD operations such as employe management, determine its roles and department and many other functions.
- The goal of this project is to demonstrate my development skills, data management, and business logic handling.

**Features**
- Create and manage employees
- Determine Employees roles and depatments
- Secured Transactions and login
- Layered architechure
- DTO based data transfer
- Validations

**Tech Stack**
- ASP.NET Core
- Entity Framework Core 
- C#
- Clean architechure principles

**What I Learned**
- Designing business rules for enterprise systems
- Designing relations between entities
- Writing clean and maintainable code
- Using EF Core effectivly
- Secured operations

**How TO Run**
1) Clone the repository
2) Configure a SQL server connection string in app.settings
3) Run Database Migrations
- For initial create (dotnet ef migrations add InitialCreate --project Data --startup-project API)
- To update (dotnet ef database update --project Data --startup-project API)
