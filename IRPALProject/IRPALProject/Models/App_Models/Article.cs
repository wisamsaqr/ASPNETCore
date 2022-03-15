using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IRPALProject.Models
{
    [MetadataType(typeof(ArticleMetadata))]
    public partial class Article
    {
        public class ArticleMetadata
        {
            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public string Title { get; set; }

            [Required (ErrorMessageResourceType = typeof(App_GlobalResources.Lang), ErrorMessageResourceName = "RequiredValMsg")]
            public string Slug { get; set; }

            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public string Details { get; set; }

            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public string Summary { get; set; }

            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            public Nullable<int> CategoryId { get; set; }
        }
    }
}