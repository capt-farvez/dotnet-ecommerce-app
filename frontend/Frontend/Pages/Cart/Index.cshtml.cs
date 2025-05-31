using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<CartItemViewModel> CartItems { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task OnGetAsync()
        {
            var cartItems = await _httpClient.GetFromJsonAsync<List<CartItemResponse>>("/api/cart");

            if (cartItems != null)
            {
                CartItems = cartItems.Select(item => new CartItemViewModel
                {
                    ProductId = item.Product.Id,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                }).ToList();
            }
        }
        // This method handles the AddToCart POST
        public async Task<IActionResult> OnPostAddToCartAsync(int productId, int quantity)
        {
            var payload = new
            {
                ProductId = productId,
                Quantity = quantity
            };

            var response = await _httpClient.PostAsJsonAsync("/api/cart/add", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage(); // reloads the page
            }

            ModelState.AddModelError(string.Empty, "Failed to add item to cart.");
            return Page();
        }
    }
}
