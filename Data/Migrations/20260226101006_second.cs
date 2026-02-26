using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDepartmentEntity_Departments_DepartmentId",
                table: "EmployeeDepartmentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDepartmentEntity_Employees_EmployeeId",
                table: "EmployeeDepartmentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleEntity_Employees_EmployeeId",
                table: "EmployeeRoleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleEntity_Roles_RoleId",
                table: "EmployeeRoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoleEntity",
                table: "EmployeeRoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeDepartmentEntity",
                table: "EmployeeDepartmentEntity");

            migrationBuilder.RenameTable(
                name: "EmployeeRoleEntity",
                newName: "EmployeeRoles");

            migrationBuilder.RenameTable(
                name: "EmployeeDepartmentEntity",
                newName: "EmployeeDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRoleEntity_RoleId",
                table: "EmployeeRoles",
                newName: "IX_EmployeeRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDepartmentEntity_DepartmentId",
                table: "EmployeeDepartments",
                newName: "IX_EmployeeDepartments_DepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Departments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles",
                columns: new[] { "EmployeeId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeDepartments",
                table: "EmployeeDepartments",
                columns: new[] { "EmployeeId", "DepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDepartments_Departments_DepartmentId",
                table: "EmployeeDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDepartments_Employees_EmployeeId",
                table: "EmployeeDepartments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoles_Employees_EmployeeId",
                table: "EmployeeRoles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoles_Roles_RoleId",
                table: "EmployeeRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDepartments_Departments_DepartmentId",
                table: "EmployeeDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDepartments_Employees_EmployeeId",
                table: "EmployeeDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoles_Employees_EmployeeId",
                table: "EmployeeRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoles_Roles_RoleId",
                table: "EmployeeRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeDepartments",
                table: "EmployeeDepartments");

            migrationBuilder.RenameTable(
                name: "EmployeeRoles",
                newName: "EmployeeRoleEntity");

            migrationBuilder.RenameTable(
                name: "EmployeeDepartments",
                newName: "EmployeeDepartmentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRoles_RoleId",
                table: "EmployeeRoleEntity",
                newName: "IX_EmployeeRoleEntity_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDepartments_DepartmentId",
                table: "EmployeeDepartmentEntity",
                newName: "IX_EmployeeDepartmentEntity_DepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoleEntity",
                table: "EmployeeRoleEntity",
                columns: new[] { "EmployeeId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeDepartmentEntity",
                table: "EmployeeDepartmentEntity",
                columns: new[] { "EmployeeId", "DepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDepartmentEntity_Departments_DepartmentId",
                table: "EmployeeDepartmentEntity",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDepartmentEntity_Employees_EmployeeId",
                table: "EmployeeDepartmentEntity",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleEntity_Employees_EmployeeId",
                table: "EmployeeRoleEntity",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleEntity_Roles_RoleId",
                table: "EmployeeRoleEntity",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
