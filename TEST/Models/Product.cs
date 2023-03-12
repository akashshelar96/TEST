using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Models
{
    public class Product
    {
        [Key] public int ProductId { get; set; }
        public int ProductName { get; set; }

        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
