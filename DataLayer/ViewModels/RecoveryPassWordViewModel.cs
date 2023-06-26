using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class RecoveryPassWordViewModel
    {
        [Display(Name = "کلمه ورود جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه ورود جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه عبور مغایرت دارد")]
        public string RePassword { get; set; }
    }
}