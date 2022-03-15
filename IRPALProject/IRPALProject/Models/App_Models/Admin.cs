using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IRPALProject.Models
{
    [MetadataType(typeof(AdminMetadata))]
    public partial class Admin
    {
        public class AdminMetadata
        {
            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public string FullName { get; set; }

            [Required(ErrorMessageResourceType = typeof(App_GlobalResources.Lang), ErrorMessageResourceName = "RequiredValMsg")]
            [EmailAddress(ErrorMessageResourceType = typeof(App_GlobalResources.Lang), ErrorMessageResourceName = "EmailValMsg")]
            public string Email { get; set; }

            //For Experiment Helper Metjods
            [Display(Name ="Admin Inserting Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:MM/yyyy/dd ss}")]
            public Nullable<System.DateTime> InsertedAt { get; set; }
        }
    }
}