using System.Security.Principal;

namespace Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime HireDate { get; set; } 
        public decimal Salary { get; set; }
        public int DepartmentId {  get; set; }
        public Department Department { get; set; } = null!;
        public int RoleId { get; set; }
        public Role Role {get;set;} = null!;
        public string Status { get; set; } = "Active";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
