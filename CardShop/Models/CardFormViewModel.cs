using CardShop.Data.Models;
using System.ComponentModel.DataAnnotations;
using static CardShop.Data.Utils.DataConstants;
using static CardShop.Data.Utils.ModelViewErrors;

namespace CardShop.Models
{
    public class CardFormViewModel
    {
        [Required(ErrorMessage = RequiredField)]
        [StringLength(CardNameMaxLength,
            MinimumLength = CardNameMinLength,
            ErrorMessage = IncorrectLength)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(CardDescriptionMaxLength,
            MinimumLength = CardDescriptionMinLength,
            ErrorMessage = IncorrectLength)]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredField)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = RequiredField)]
        public int GameId { get; set; }

        public IList<GameViewModel> Games { get; set; } = new List<GameViewModel>();
    }
}
