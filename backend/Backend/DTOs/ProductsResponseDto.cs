namespace Backend.DTOs
{
    public class ProductsResponseDto
    {
        public int TotalItems { get; set; }
        public int TotalVendors { get; set; }
        public List<ProductDto> Products { get; set; } = new();
    }
}