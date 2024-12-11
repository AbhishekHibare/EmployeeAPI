using EmployeeAPI.Interfaces;
using EmployeeAPI.Models;
using System.Linq;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;
        private readonly List<AuditLog> _auditLogs;

        public EmployeeService()
        {
            _employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice", Age = 30, Department = "HR", PhoneNumber = "1234567890", Salary = 50000 },
                new Employee { Id = 2, Name = "Bob", Age = 25, Department = "IT", PhoneNumber = "9876543210", Salary = 60000 },
                new Employee { Id = 3, Name = "Charlie", Age = 35, Department = "Finance", PhoneNumber = "5678901234", Salary = 70000 }
            };

            _auditLogs = new List<AuditLog>();
        }

        public IEnumerable<Employee> GetEmployeesSortedByAge()
        {
            return _employees.OrderBy(e => e.Age);
        }

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            AddAuditLog(employee.Id.ToString(), "Add", $"Added employee {employee.Name}.");
        }

        public bool UpdateEmployee(Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee == null) return false;

            var changes = GetChanges(existingEmployee, employee);

            existingEmployee.Name = employee.Name;
            existingEmployee.Age = employee.Age;
            existingEmployee.Department = employee.Department;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.Salary = employee.Salary;

            AddAuditLog(employee.Id.ToString(), "Update", changes);
            return true;
        }

        public bool UpdateEmployeeFields(int id, string phoneNumber, decimal salary)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null) return false;

            var changes = $"PhoneNumber: {existingEmployee.PhoneNumber} -> {phoneNumber}, Salary: {existingEmployee.Salary} -> {salary}";

            existingEmployee.PhoneNumber = phoneNumber;
            existingEmployee.Salary = salary;

            AddAuditLog(id.ToString(), "Update Fields", changes);
            return true;
        }

        public IEnumerable<AuditLog> GetAuditLogs()
        {
            return _auditLogs;
        }

        private void AddAuditLog(string employeeId, string action, string details)
        {
            _auditLogs.Add(new AuditLog
            {
                LogId = _auditLogs.Count + 1,
                Timestamp = DateTime.UtcNow,
                EmployeeId = employeeId,
                Action = action,
                Details = details
            });
        }

        private string GetChanges(Employee oldEmployee, Employee newEmployee)
        {
            var changes = new List<string>();

            if (oldEmployee.Name != newEmployee.Name)
                changes.Add($"Name: {oldEmployee.Name} -> {newEmployee.Name}");

            if (oldEmployee.Age != newEmployee.Age)
                changes.Add($"Age: {oldEmployee.Age} -> {newEmployee.Age}");

            if (oldEmployee.Department != newEmployee.Department)
                changes.Add($"Department: {oldEmployee.Department} -> {newEmployee.Department}");

            if (oldEmployee.PhoneNumber != newEmployee.PhoneNumber)
                changes.Add($"PhoneNumber: {oldEmployee.PhoneNumber} -> {newEmployee.PhoneNumber}");

            if (oldEmployee.Salary != newEmployee.Salary)
                changes.Add($"Salary: {oldEmployee.Salary} -> {newEmployee.Salary}");

            return string.Join(", ", changes);
        }
    }
}
