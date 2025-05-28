using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products?page=1&size=10&search=keyword
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? search = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            int totalItems = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.ImageUrl,
                    Price = (p.DiscountStart.HasValue && p.DiscountEnd.HasValue
                             && DateTime.UtcNow >= p.DiscountStart.Value
                             && DateTime.UtcNow <= p.DiscountEnd.Value
                             && p.DiscountPrice.HasValue)
                            ? p.DiscountPrice.Value
                            : p.Price,
                    p.VendorId
                })
                .ToListAsync();

            return Ok(new { totalItems, products });
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            var price = (product.DiscountStart.HasValue && product.DiscountEnd.HasValue
                        && DateTime.UtcNow >= product.DiscountStart.Value
                        && DateTime.UtcNow <= product.DiscountEnd.Value
                        && product.DiscountPrice.HasValue)
                        ? product.DiscountPrice.Value
                        : product.Price;

            return Ok(new
            {
                product.Id,
                product.Name,
                product.ImageUrl,
                Price = price,
                product.VendorId
            });
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = updatedProduct.Name;
            product.ImageUrl = updatedProduct.ImageUrl;
            product.Price = updatedProduct.Price;
            product.DiscountPrice = updatedProduct.DiscountPrice;
            product.DiscountStart = updatedProduct.DiscountStart;
            product.DiscountEnd = updatedProduct.DiscountEnd;
            product.VendorId = updatedProduct.VendorId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
