using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Users_Metadata
    {
        public long UserId { get; set; }
        [Display(Name = "نقش کاربر")]
        public int RoleId { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        [Display(Name = "کد فعال سازی")]
        public string ActiveCode { get; set; }
        [Display(Name = " وضعیت  ")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public System.DateTime RegisterDate { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
