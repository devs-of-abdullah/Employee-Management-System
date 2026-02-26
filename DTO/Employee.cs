 using System.ComponentModel.DataAnnotations;

namespace DTO.Employee
{
    public record CreateEmployeeDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; init; } = null!;

        [Required, MaxLength(50)]
        public string LastName { get; init; } = null!;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; init; } = null!;

        [Required, MaxLength(16)]
        public string PhoneNumber { get; init; } = null!;

        [Required]
        public DateTime HireDate { get; init; }

        [Required]
        public decimal Salary { get; init; }

        public int[]? DepartmentIDs { get; init; }
        public int[]? RoleIDs { get; init; }
    }

    public record ReadEmployeeDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? PhoneNumber { get; init; }
        public DateTime HireDate { get; init; }
        public decimal Salary { get; init; }
        public bool IsActive { get; init; }
        public int[]? DepartmentIDs { get; init; }
        public int[]? RoleIDs { get; init; }
    }

    public record UpdateEmployeeDto
    {
        [MaxLength(50)]
        public string? FirstName { get; init; }

        [MaxLength(50)]
        public string? LastName { get; init; }

        [MaxLength(16)]
        public string? PhoneNumber { get; init; }

        public decimal? Salary { get; init; }
    }
}