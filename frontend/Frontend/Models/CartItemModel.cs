namespace Frontend.Models
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductResponse Product { get; set; } = new();
    }

    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public decimal Price { get; set; }
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
