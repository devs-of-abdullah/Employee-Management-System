namespace Entities.DTOs
{
    
    
    public class CreateEmployeeDto
    {
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
            public DateTime HireDate { get; set; }
            public decimal Salary { get; set; }
            public int DepartmentId { get; set; }
            public int RoleId { get; set; }
    }
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
    
}
