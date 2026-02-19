using Entities;

namespace Entity
{
    public class DepartmentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public ICollection<EmployeeDepartmentEntity> EmployeeDepartments { get; set; } = new List<EmployeeDepartmentEntity>();
    }
}