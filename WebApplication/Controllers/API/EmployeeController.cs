using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.DTO;
using WebApplication.Models;

namespace WebApplication.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PitangueiraContext _context;
        public EmployeeController(PitangueiraContext context)
        {
            _context = context;
        }

        [Route("Task")]
        [HttpGet]
        [Authorize(Roles = "Manager, Technician")]
        public async Task<IActionResult> EmployeeTasks()
        {
            var employeeName = User.Identity.Name;
            var Employee = await _context.Employees.FirstOrDefaultAsync( e => e.UserName == employeeName);
            if (Employee == null)
                return BadRequest("Error");
            Employee.Role = await _context.Roles.FirstOrDefaultAsync( r => r.Id == Employee.RoleId);
            var Attendances = _context.Attendances.Where(a => a.Employee.Id == Employee.Id).ToList();
            foreach(var attendance in Attendances)
            {
                attendance.Type = _context.Types.FirstOrDefault(t => t.Id == attendance.TypeId);
            }
            Employee.Attendances = Attendances;
            return Ok(Employee);
        }

        [Route("AllTasks")]
        [HttpGet]
        [Authorize(Roles = "Manager, Technician")]
        public async Task<IActionResult> AllTasks()
        {
            var employeeName = User.Identity.Name;
            var user = await _context.Employees.FirstOrDefaultAsync(e=> e.UserName == employeeName);
            user.Role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
            var Attendances = await _context.Attendances.ToListAsync();
            foreach (var attendance in Attendances)
            {
                attendance.Type = await _context.Types.FirstOrDefaultAsync(t => t.Id == attendance.TypeId);
                attendance.Employee = await _context.Employees.FirstAsync( e=> e.Id == attendance.EmployeeId);
            }
            user.Attendances = Attendances;
            EmployeeRelatory u = new EmployeeRelatory();
            u.Name = user.UserName;
            u.Role = user.Role.Name;
            u.Attendances =  Attendances;
            return Ok(u);

        }

        [Route("AllTask")]
        [HttpGet]
        [Authorize(Roles = "Manager, Technician")]
        public async Task<IActionResult> EmployeeAllTasks()
        {
            var employeeName = User.Identity.Name;
            var Employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserName == employeeName);
            if (Employee == null)
                return BadRequest("Error");
            var Attendances = await _context.Attendances.Where(a => a.Employee.Id == Employee.Id).ToListAsync();
            return Ok(Attendances);
        }

        [Route("CreateTask")]
        [HttpPost]
        [Authorize(Roles = "Manager, Technician")]
        public async Task<IActionResult> CreateTask([FromBody] AttendanceCreate Attendance)
        {
            var employeeName = User.Identity.Name;
            Attendance attendance = new Attendance();
            attendance.Client = Attendance.Clients;
            attendance.Comments = Attendance.Comments;
            attendance.Type =  await _context.Types.FirstOrDefaultAsync(t => t.Name == Attendance.Type);
            var Employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserName == employeeName);
            if (Employee == null)
                return BadRequest("Error");
            attendance.Employee = Employee;
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
            return Ok();
        }



        [Route("DeleteTask")]
        [HttpPost]
        [Authorize(Roles = "Manager, Technician")]
        public async Task<IActionResult> DeleteTask([FromBody] int Id)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.Id == Id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                _context.SaveChanges();
            }
            return Ok();
        }



        [Route("Types")]
        [HttpGet]
        [Authorize(Roles = "Manager,Technician")]
        public async Task<IActionResult> GetTypes()
        {
            var Types = await  _context.Types.ToListAsync();
            return Ok(Types);
        }

        [Route("TypesManagement")]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ManageTypes()
        {
            var Name = User.Identity.Name;
            EmployeewTypes response = new EmployeewTypes();
            var Employee = await _context.Employees.FirstOrDefaultAsync( e => e.UserName ==Name);
            var Types = await _context.Types.ToListAsync();
            var Role =  await _context.Roles.FirstOrDefaultAsync(r => r.Id == Employee.RoleId);
            response.UserName = Name;
            response.Role = Role;
            response.Types = Types;
            return Ok(response);
        }

        [Route("CreateType")]
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateType([FromBody] AttenType attendanceType)
        {
            _context.Types.Add(attendanceType);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("DeleteType")]
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteType([FromBody] int Id)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.Type.Id == Id);
            if (attendance == null)
            {
                var type = await _context.Types.FirstOrDefaultAsync(a => a.Id == Id);
                if (type != null)
                {
                    _context.Types.Remove(type);
                    _context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
