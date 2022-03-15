using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Models
{
    public class User1
    {
        // Creating 1-1 relationship without ManagerId prop
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
