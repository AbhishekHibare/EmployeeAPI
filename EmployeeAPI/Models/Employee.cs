namespace EmployeeAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Department { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Salary { get; set; }
    }
}
