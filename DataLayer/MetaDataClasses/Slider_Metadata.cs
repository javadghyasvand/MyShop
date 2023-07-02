using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Slider_Metadata
    {
        [Key]

        public int SliderID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string SliderTitle { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Url(ErrorMessage = "آدرس وارد شده معتبر نمی باشد")]

        public string Url { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]

        public System.DateTime StartDate { get; set; }

        [Display(Name = "تاریخ اتمام ")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public System.DateTime EndDate { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public bool IsActive { get; set; }

        [Display(Name = "تصویر")]

        public string ImageName { get; set; }
    }
}