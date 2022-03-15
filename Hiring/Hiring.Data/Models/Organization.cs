using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Data.Models
{
    public class Organization : BaseEntity
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string WorkNature { get; set; }
        
        [Required]
        public string Address { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public List<JobAdvertisement> JobAdvertisements { get; set; }
    }
}