namespace DTO.Department
{
    using System.ComponentModel.DataAnnotations;
    using DTO.Employee;

    public class CreateDepartmentDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateDepartmentNameDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
    }

    public class UpdateDepartmentDescriptionDto
    {
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }

    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public List<ReadEmployeeDto> Employees { get; set; } = new();
    }
}
