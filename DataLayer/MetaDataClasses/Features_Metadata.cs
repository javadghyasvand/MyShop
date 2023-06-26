using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Features_Metadata
    {
        [Key]
        public int FeatureId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان ویژگی")]
        public string FeaturesTitle { get; set; }
    }
}
