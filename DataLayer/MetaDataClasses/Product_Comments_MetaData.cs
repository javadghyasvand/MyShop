using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_Comment_Metadata
    {
        [Key]
        public int CommentID { get; set; }
        [Display(Name ="نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "متن نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Comment { get; set; }
    }
}
