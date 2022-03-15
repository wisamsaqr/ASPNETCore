using Hiring.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CvUrl { get; set; }
        public UserType Type { get; set; }

        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}