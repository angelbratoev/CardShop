using CardShop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Data
{
    public class CardShopDbContext : IdentityDbContext
    {
        public CardShopDbContext(DbContextOptions<CardShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<CardBuyer> CardsBuyers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CardBuyer>()
                .HasKey(pk => new { pk.BuyerId, pk.CardId });

            builder.Entity<CardBuyer>()
                .HasOne(cb => cb.Card)
                .WithMany(c => c.CardsBuyers)
                .HasForeignKey(cb => cb.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Game>()
                .HasData(
                new Game()
                {
                    Id = 1,
                    Name = "Yu-Gi-Oh!"
                },
                new Game()
                {
                    Id = 2,
                    Name = "Pokemon"
                },
                new Game()
                {
                    Id = 3,
                    Name = "Magic The Gathering"
                },
                new Game()
                {
                    Id = 4,
                    Name = "Flesh and Blood"
                });

            base.OnModelCreating(builder);
        }
    }
}