using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه ورود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه ورود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "کلمه عبور مغایرت دارد")]
        public string RePassword  { get; set; }

    }
}
