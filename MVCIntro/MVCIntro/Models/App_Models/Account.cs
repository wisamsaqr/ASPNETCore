using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCIntro.Models
{
    [MetadataType(typeof(AccountMetaData))]
    public partial class Account
    {
        public class AccountMetaData
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Gender { get; set; }

            [Required]
            public Nullable<bool> Active { get; set; }

            [Display(Name = "Country")]
            [Required]
            public Nullable<int> CountryId { get; set; }

            [Required]
            [Display(Name = "Birth Date")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
            public Nullable<System.DateTime> DOB { get; set; }

            public virtual Country Country { get; set; }
        }
    }
}