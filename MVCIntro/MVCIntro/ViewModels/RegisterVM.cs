using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCIntro.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessageResourceName = "RequiredValidationMsg",
            ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
        [Display(Name = "FullName", ResourceType = typeof(App_GlobalResources.Lang))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredValidationMsg",
            ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
        [EmailAddress(ErrorMessageResourceName = "EmailValidationMsg",
            ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
        [Display(Name = "Email", ResourceType = typeof(App_GlobalResources.Lang))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredValidationMsg",
            ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
        [Display(Name = "Mobile", ResourceType = typeof(App_GlobalResources.Lang))]
        public string Mobile { get; set; }

        [Required(ErrorMessageResourceName = "RequiredValidationMsg",
            ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
        [Display(Name = "Address", ResourceType = typeof(ZMyResources.Lang))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "RequiredValidationMsg",
            ErrorMessageResourceType = typeof(ZMyResources.Lang))]
        [Display(Name = "Gender", ResourceType = typeof(ZMyResources.Lang))]
        public string Gender { get; set; }
    }
}