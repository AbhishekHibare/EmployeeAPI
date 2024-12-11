using EmployeeAPI.Interfaces;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeService.GetEmployeesSortedByAge();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            _employeeService.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployees), new { id = employee.Id }, employee);
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            var updated = _employeeService.UpdateEmployee(employee);
            if (!updated) return NotFound($"Employee with ID {employee.Id} not found.");
            return NoContent();
        }

        [HttpPut("{id}/update-fields")]
        public IActionResult UpdateEmployeeFields(int id, [FromBody] UpdateFieldsRequest request)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var updated = _employeeService.UpdateEmployeeFields(id, phoneNumber: request.PhoneNumber, request.Salary);
#pragma warning restore CS8604 // Possible null reference argument.
            if (!updated) return NotFound($"Employee with ID {id} not found.");
            return NoContent();
        }

        [HttpGet("audit-logs")]
        public IActionResult GetAuditLogs()
        {
            var logs = _employeeService.GetAuditLogs();
            return Ok(logs);
        }
    }

    public class UpdateFieldsRequest
    {
        public string? PhoneNumber { get; set; }
        public decimal Salary { get; set; }
    }
}
