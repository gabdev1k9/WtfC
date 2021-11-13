using System;

namespace WebApplication.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public string Comments { get; set; }
        public DateTime ExecutionDate { get; set; }
        public int TypeId { get; set; }
        public AttenType Type { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}