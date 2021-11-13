using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DTO;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly PitangueiraContext _context;
        public TokenController(IConfiguration Config, PitangueiraContext context)
        {
            Configuration = Config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateToken([FromBody] User User)
        {
            var Employee = await _context.Employees.FirstOrDefaultAsync( e=> e.UserName == User.UserName && e.Password == User.Password);
            if (Employee == null)
                return BadRequest("Error");
            Employee.Role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == Employee.RoleId);
            var token = TokenEmployee(Employee);
            return Ok(token);
        }

        private string TokenEmployee(Employee Employee)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {   
                    new Claim("Id", Employee.Id.ToString()),
                    new Claim(ClaimTypes.Name, Employee.UserName),
                    new Claim(ClaimTypes.Role, Employee.Role.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
