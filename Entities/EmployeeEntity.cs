
namespace Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<EmployeeDepartmentEntity> EmployeeDepartments { get; set; } = new List<EmployeeDepartmentEntity>();
        public ICollection<EmployeeRoleEntity> EmployeeRoles { get; set; } = new List<EmployeeRoleEntity>();
    }
}
