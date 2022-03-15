using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IRPALProject.Models
{
    [MetadataType(typeof(SliderMetadata))]
    public partial class Slider
    {
        public class SliderMetadata
        {
            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public string Title { get; set; }
        }
    }
}