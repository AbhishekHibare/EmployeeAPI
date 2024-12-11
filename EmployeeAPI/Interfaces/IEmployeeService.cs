using EmployeeAPI.Models;

namespace EmployeeAPI.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployeesSortedByAge();
        void AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool UpdateEmployeeFields(int id, string phoneNumber, decimal salary);
        IEnumerable<AuditLog> GetAuditLogs();
    }
}
