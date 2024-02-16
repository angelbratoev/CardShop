using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CardShop.Data.Utils.DataConstants;

namespace CardShop.Data.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CardNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(CardDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int GameId { get; set; }

        [Required]
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;

        public IList<CardBuyer> CardsBuyers { get; set; } = new List<CardBuyer>();
    }
}
