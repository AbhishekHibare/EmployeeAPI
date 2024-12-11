namespace EmployeeAPI.Models
{
    public class AuditLog
    {
        public int LogId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? EmployeeId { get; set; }
        public string? Action { get; set; }
        public string? Details { get; set; }
    }
}
