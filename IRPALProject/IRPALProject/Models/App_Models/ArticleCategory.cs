using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IRPALProject.Models
{
    [MetadataType(typeof(ArticleCategoryMetadata))]
    public partial class ArticleCategory
    {
        public class ArticleCategoryMetadata
        {
            [Required(ErrorMessageResourceName = "RequiredValMsg", ErrorMessageResourceType = typeof(App_GlobalResources.Lang))]
            [Display(ResourceType = typeof(App_GlobalResources.Lang), Name = "ArticleCategoryTitle")]
            public string Title { get; set; }

            //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            //public Nullable<System.DateTime> InsertedAt { get; set; }
        }
    }
}