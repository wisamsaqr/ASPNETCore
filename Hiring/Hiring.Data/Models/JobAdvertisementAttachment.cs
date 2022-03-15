using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiring.Data.Models
{
    public class JobAdvertisementAttachment
    {
        public int Id { get; set; }
        
        [Required]
        public string Url { get; set; }

        public int JobAdvertisementId { get; set; }
        public JobAdvertisement JobAdvertisement { get; set; }
    }
}