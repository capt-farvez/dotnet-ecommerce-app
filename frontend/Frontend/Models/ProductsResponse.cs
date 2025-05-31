namespace Frontend.Models
{
    public class ProductsResponse
    {
        public int TotalItems { get; set; }
        public int TotalVendors { get; set; }
        public List<ProductViewModel> Products { get; set; } = new();
    }
}