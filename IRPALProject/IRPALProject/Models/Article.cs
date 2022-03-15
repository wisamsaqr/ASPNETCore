//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IRPALProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public bool Published { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> InsertedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> InsertingAdminId { get; set; }
        public Nullable<int> UpdatingAdminId { get; set; }
    
        public virtual ArticleCategory ArticleCategory { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Admin Admin1 { get; set; }
    }
}
