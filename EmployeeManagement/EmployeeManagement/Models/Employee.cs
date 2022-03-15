using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50, ErrorMessage = "Name cann't exceed 50 characters.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Enter the name pleas..")]
        [EmailAddress]
        [Display(Name = "Office Email")]
        
        public string Email { get; set; }
        
        public Dept? Department { get; set; }

        public string PhotoPath { get; set; }
    }
}
