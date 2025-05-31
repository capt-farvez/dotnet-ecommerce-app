using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/vendors")]
    public class VendorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/vendors
        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            var vendors = await _context.Vendors
                .Include(v => v.Products)
                .Select(v => new {
                    v.Id,
                    v.Name,
                    v.IsAvailable,
                    Products = v.Products.Select(p => new {
                        p.Id,
                        p.Name,
                        p.Price,
                        p.DiscountPrice
                    })
                })
                .ToListAsync();

            return Ok(vendors);
        }



        // POST: api/vendors
        [HttpPost]
        public async Task<IActionResult> AddVendor([FromBody] Vendor vendor)
        {
            if (vendor == null)
                return BadRequest();

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVendors), new { id = vendor.Id }, vendor);
        }
    }
}
