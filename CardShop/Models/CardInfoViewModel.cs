﻿namespace CardShop.Models
{
    public class CardInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Game { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}
