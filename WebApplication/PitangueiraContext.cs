using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication
{
    public class PitangueiraContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttenType> Types { get; set; }

        public PitangueiraContext(DbContextOptions<PitangueiraContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasOne(e => e.Role).WithMany(r => r.Employees).HasForeignKey( e => e.RoleId);

            modelBuilder.Entity<Employee>().HasMany(e => e.Attendances).WithOne(a => a.Employee).HasForeignKey( a => a.EmployeeId);

            modelBuilder.Entity<Attendance>().Property(a => a.ExecutionDate).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Attendance>().HasOne(a => a.Type).WithMany(t => t.Attendances).HasForeignKey( a => a.TypeId);
        }
    }
}
