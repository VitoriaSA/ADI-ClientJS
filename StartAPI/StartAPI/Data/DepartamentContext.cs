using Microsoft.EntityFrameworkCore;
using StartAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartAPI.Data
{
    public class DepartamentContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=123456;Persist Security Info=False;User ID=admin;Initial Catalog=StartDB;Data Source=DESKTOP-T9EB9G3\\SQLEXPRESS");
        }
    }
}
