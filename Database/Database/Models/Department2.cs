using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Department2
    {
        // Creating 1-1 relationship with ManagerId prop
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int ManagerId { get; set; }
        public User2 Manager { get; set; }
    }
}
