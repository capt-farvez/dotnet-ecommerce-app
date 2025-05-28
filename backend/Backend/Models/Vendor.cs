namespace Backend.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsAvailable { get; set; } = true;

        // Navigation property (1 vendor → many products)
        public List<Product> Products { get; set; } = new();
    }
}