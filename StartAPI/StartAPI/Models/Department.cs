using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartAPI.Models
{
    public class Department
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Initals { get; set; }
        public List<Employee> Employees { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
