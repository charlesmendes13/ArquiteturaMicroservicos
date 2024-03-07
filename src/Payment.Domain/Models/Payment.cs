using System.ComponentModel.DataAnnotations;

namespace Payment.Domain.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int BasketId { get; set; }
        [Required]
        public double Total { get; set; }
    }
}
