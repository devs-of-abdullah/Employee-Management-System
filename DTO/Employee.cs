namespace DTO.Employee
{
    
    
    public record CreateEmployeeDto
    {
            public string FirstName { get; init; } = null!;
            public string LastName { get; init; } = null!;
            public string Email { get; init; } = null!;
            public string PhoneNumber { get; init; } = null!;
            public DateTime HireDate { get; init; }
            public decimal Salary { get; init; }
            public int[]? DepartmentIDs { get; set; }
            public int[]? RoleIDs { get; set; }
    }
    public record ReadEmployeeDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? PhoneNumber { get; init; } = null!;
        public DateTime HireDate { get; init; }
        public decimal Salary { get; init; }
        public int[]? DepartmentIDs { get; set; }
        public int[]? RoleIDs {  get; set; } 
    }
    
}
