//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DataLayer
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(Product_Comment_Metadata))]
    public partial class Product_Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_Comment()
        {
            this.Product_Comment1 = new HashSet<Product_Comment>();
        }


        public int CommentID { get; set; }

        public int ProductID { get; set; }

        public Nullable<int> ParnetID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public System.DateTime CommentDate { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Product_Comment> Product_Comment1 { get; set; }

        public virtual Product_Comment Product_Comment2 { get; set; }

        public virtual Products Products { get; set; }
    }
}