namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public int Qunatity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
    }
}
