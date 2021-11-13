using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.DTO
{
    public class AttendanceCreate
    {
        public string Clients { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
    }
}
