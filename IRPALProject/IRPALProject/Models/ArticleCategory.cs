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
    
    public partial class ArticleCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArticleCategory()
        {
            this.Articles = new HashSet<Article>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> InsertedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> InsertingAdminId { get; set; }
        public Nullable<int> UpdatingAdminId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Admin Admin1 { get; set; }
    }
}
