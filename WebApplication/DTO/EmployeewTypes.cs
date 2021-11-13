using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.DTO
{
    public class EmployeewTypes
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
        public ICollection<AttenType> Types {get;set;}
    }
}
