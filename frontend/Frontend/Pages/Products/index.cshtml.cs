using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.Models;

namespace Frontend.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public List<ProductViewModel> Products { get; set; } = new();

        public int TotalProducts => Products.Count;
        public int TotalItems { get; set; }
        public int AvailableVendors => Products.Select(p => p.VendorId).Distinct().Count();
        public int TotalVendors { get; set; }
        public int UniqueProductCount => Products.Select(p => p.Name).Distinct().Count();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ApiClient");
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ProductsResponse>("/api/products");

            if (response != null)
            {
                Products = response.Products;
                TotalVendors = response.TotalVendors;
                TotalItems = response.TotalItems;
            }

        }
    }
}
