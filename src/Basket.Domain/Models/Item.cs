﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.Domain.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("Basket")]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
