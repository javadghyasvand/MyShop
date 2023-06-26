using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_Tag_Metadata
    {
        public int TagID { get; set; }
        public int ProductID { get; set; }
        public string Tag { get; set; }
    } 
}