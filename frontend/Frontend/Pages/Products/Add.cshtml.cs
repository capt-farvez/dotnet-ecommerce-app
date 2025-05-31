using Frontend.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;


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
