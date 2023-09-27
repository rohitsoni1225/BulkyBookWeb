using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(5,100 , ErrorMessage ="Order must be between 5 to 100")]
        public int DisplayOrder { get; set; }
    
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
