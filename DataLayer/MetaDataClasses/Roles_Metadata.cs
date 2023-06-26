using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Roles_Metadata
    {
        [Key]
        public int RoleID { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleTitle { get; set; }
        [Display(Name = "عنوان سیستمی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleName { get; set; }

    }
}