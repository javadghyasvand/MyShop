
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
    
[MetadataType(typeof(Features_Metadata))]
    public partial class Features
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Features()
    {

        this.Product_Featuers = new HashSet<Product_Featuers>();

    }


    public int FeatureId { get; set; }

    public string FeaturesTitle { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Product_Featuers> Product_Featuers { get; set; }

}

}
