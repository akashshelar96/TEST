using System.ComponentModel.DataAnnotations;

namespace TEST.Models
{
    public class Category
    {
        [Key] public int CategoryId { get; set; }

        public int CategoryName { get; set; }
    }
}
