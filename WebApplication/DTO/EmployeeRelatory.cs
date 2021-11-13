using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.DTO
{
    public class EmployeeRelatory
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Attendance> Attendances { get; set; }
    }
}
