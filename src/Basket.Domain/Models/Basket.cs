using System.ComponentModel.DataAnnotations;

namespace Basket.Domain.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
