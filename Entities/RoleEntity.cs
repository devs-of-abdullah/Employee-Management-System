

namespace Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;

        public ICollection<EmployeeRoleEntity> EmployeeRoles { get; set; } = new List<EmployeeRoleEntity>();
    }
}