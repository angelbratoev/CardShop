using CardShop.Data;
using CardShop.Data.Models;
using CardShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Claims;
using System.Xml.Linq;

namespace CardShop.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private CardShopDbContext data;

        public CardController(CardShopDbContext _data)
        {
            data = _data;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cards = await data.Cards
                .AsNoTracking()
                .Select(c => new CardInfoViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl,
                    Game = c.Game.Name,
                    Owner = c.Owner.UserName
                })
                .ToListAsync();

            return View(cards);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CardFormViewModel();
            model.Games = await GetGamesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CardFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Games = await GetGamesAsync();
            }

            Card card = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                OwnerId = GetUserId(),
                ImageUrl = model.ImageUrl,
                GameId = model.GameId
            };

            await data.Cards.AddAsync(card);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Cards
                .Where(c => c.Id == id)
                .Select(c => new CardInfoViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl,
                    Owner = c.Owner.UserName,
                    Game = c.Game.Name
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var model = await data.Cards
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CardFormViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    OwnerId = c.OwnerId,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl,
                    GameId = c.GameId
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            if (model.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            model.Games = await GetGamesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CardFormViewModel model, int id)
        {
            Card card = await data.Cards
                .FindAsync(id);

            if (card == null)
            {
                return BadRequest();
            }

            if (GetUserId() != card.OwnerId)
            {
                return Unauthorized();
            }

            card.Name = model.Name;
            card.Description = model.Description;
            card.OwnerId = GetUserId();
            card.Price = model.Price;
            card.ImageUrl = model.ImageUrl;
            card.GameId = model.GameId;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            string userId = GetUserId();

            var model = await data.Cards
                .Where(c => c.Id == id)
                .Include(c => c.CardsBuyers)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            if (!model.CardsBuyers.Any(cb => cb.BuyerId == userId))
            {
                model.CardsBuyers.Add(new CardBuyer()
                {
                    BuyerId = userId,
                    CardId = id
                });
            }

            await data.SaveChangesAsync();

            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IList<GameViewModel>> GetGamesAsync()
        {
            return await data.Games
                .AsNoTracking()
                .Select(g => new GameViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToListAsync();
        }
    }
}
