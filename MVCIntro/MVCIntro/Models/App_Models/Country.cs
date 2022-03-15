using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCIntro.Models
{
    [MetadataType(typeof(CountryMD))]
    public partial class Country
    {
        public class CountryMD
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Iso2 { get; set; }

            [Required]
            public string Code { get; set; }
        }
    }
}