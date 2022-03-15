using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTraining.Models
{
    public class Employee : BaseModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime Birthdate { get; set; }
        
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
