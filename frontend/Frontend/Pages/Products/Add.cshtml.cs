using Frontend.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Pages.Products
{
    public class AddModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public AddModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        [BindProperty]
        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _httpClient.PostAsJsonAsync("/api/products", Product);
            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Products/Index");

            ModelState.AddModelError(string.Empty, "Failed to add product.");
            return Page();
        }
    }
}
