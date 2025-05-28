using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .ToListAsync();

            var result = cartItems.Select(ci => new
            {
                ci.Id,
                ci.Quantity,
                Product = new
                {
                    ci.Product.Id,
                    ci.Product.Name,
                    ci.Product.ImageUrl,
                    Price = (ci.Product.DiscountStart.HasValue && ci.Product.DiscountEnd.HasValue
                            && DateTime.UtcNow >= ci.Product.DiscountStart.Value
                            && DateTime.UtcNow <= ci.Product.DiscountEnd.Value
                            && ci.Product.DiscountPrice.HasValue)
                            ? ci.Product.DiscountPrice.Value
                            : ci.Product.Price
                }
            });

            return Ok(result);
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
        {
            if (cartItem == null || cartItem.ProductId <= 0 || cartItem.Quantity <= 0)
                return BadRequest("Invalid CartItem data.");

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }
            else
            {
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/cart/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItem updatedItem)
        {
            if (id != updatedItem.Id)
                return BadRequest();

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            cartItem.Quantity = updatedItem.Quantity;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/cart/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
