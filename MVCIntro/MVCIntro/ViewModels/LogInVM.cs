using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCIntro.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please Enter UserName")]
        public String UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}