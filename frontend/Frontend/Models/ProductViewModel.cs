namespace Frontend.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public decimal Price { get; set; }
        public int VendorId { get; set; }
        public DateTime? DiscountStart { get; set; }
        public DateTime? DiscountEnd { get; set; }

        public bool IsDiscounted =>
            DiscountStart.HasValue &&
            DiscountEnd.HasValue &&
            DateTime.UtcNow >= DiscountStart.Value &&
            DateTime.UtcNow <= DiscountEnd.Value;
    }
}