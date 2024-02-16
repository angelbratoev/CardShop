using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardShop.Data.Models
{
    public class CardBuyer
    {
        [Required]
        public int CardId { get; set; }

        [Required]
        [ForeignKey(nameof(CardId))]
        public Card Card { get; set; } = null!;

        [Required]
        public string BuyerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(BuyerId))]
        public IdentityUser Buyer { get; set; } = null!;
    }
}
