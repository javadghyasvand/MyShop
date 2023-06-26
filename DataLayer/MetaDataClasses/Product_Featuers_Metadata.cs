using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_Featuers_Metadata
    {
        [Key]
        public int Product_Featuer_Id { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "محصول")]
        public int Product_Id { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ویژگی")]
        public int Featuers_Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مقدار")]
        public string Value { get; set; }
    }
}
