using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartAPI.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string RG { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        
    }
}
