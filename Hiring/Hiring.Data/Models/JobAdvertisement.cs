using Hiring.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Data.Models
{
    public class JobAdvertisement : BaseEntity
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public List<JobAdvertisementAttachment> Attachments { get; set; }

        public JobAdvertisementStatus Status { get; set; }
    }
}