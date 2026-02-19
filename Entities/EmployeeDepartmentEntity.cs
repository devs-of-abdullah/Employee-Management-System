

using Entity;

namespace Entities
{
    public class EmployeeDepartmentEntity
    {
        public int EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; } = null!;
        public int DepartmentId { get; set; }
        public DepartmentEntity Department { get; set; } = null!;
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    }

}
