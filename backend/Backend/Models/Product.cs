namespace Backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public DateTime? DiscountStart { get; set; }
        public DateTime? DiscountEnd { get; set; }

        public int VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        public List<CartItem> CartItems { get; set; } = new();
    }
}