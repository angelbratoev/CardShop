using CardShop.Data.Utils;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.GameNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IList<Card> Cards { get; set; } = new List<Card>();
    }
}
