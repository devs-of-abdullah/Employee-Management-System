namespace DTO.Role
{
    using System.ComponentModel.DataAnnotations;
    using DTO.Employee;

    public class CreateRoleDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateRoleNameDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
    }

    public class UpdateRoleDescriptionDto
    {
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }

    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public List<ReadEmployeeDto> Employees { get; set; } = new();
    }
}