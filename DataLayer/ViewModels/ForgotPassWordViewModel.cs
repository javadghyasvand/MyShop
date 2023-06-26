using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class ForgotPassWordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }
    }
}