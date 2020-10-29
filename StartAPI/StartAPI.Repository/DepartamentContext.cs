using Microsoft.EntityFrameworkCore;
using StartAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartAPI.Repository
{
    public class DepartamentContext : DbContext
    {

        public DepartamentContext(DbContextOptions<DepartamentContext> options) : base(options){}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
