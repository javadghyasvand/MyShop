using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_Groups_Metadata
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string GroupTitle { get; set; }
        public Nullable<int> ParentId { get; set; }
    }
}
