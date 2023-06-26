using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_Galleries_Metadata
    {
        [Key]
        public int GalleryID { get; set; }
        [Display(Name = "کالا")]
        public int ProductID { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "عنوان")]
        [Required]
        public string Title { get; set; }
    }
}
