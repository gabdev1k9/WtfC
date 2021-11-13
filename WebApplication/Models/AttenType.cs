using System.Collections.Generic;

namespace WebApplication.Models
{
    public class AttenType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}