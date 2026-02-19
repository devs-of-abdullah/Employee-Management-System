

using System.Data;

namespace Entities
{
    public class EmployeeRoleEntity
    {
        public int EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; } = null!;

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; } = null!;

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    }

}
