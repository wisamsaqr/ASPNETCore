﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Department3
    {
        // Creating 1-1 relationship with props in the 2 models
        public int Id { get; set; }
        public string Name { get; set; }

        public int ManagerId { get; set; }
        public User3 Manager { get; set; }
    }
}
