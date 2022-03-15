using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IRPALProject.Models
{
    [MetadataType(typeof(StaticPageMetadata))]
    public partial class StaticPage
    {
        public class StaticPageMetadata
        {
            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public string Title { get; set; }

            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public string Details { get; set; }
        }
    }
}