using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly APIDatabaseContext _context;

        public CartsController(APIDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carts>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carts>> GetCarts(int id)
        {
            var carts = await _context.Carts.FindAsync(id);

            if (carts == null)
            {
                return NotFound();
            }

            return carts;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Carts>>> GetUserCarts(int userId)
        {
            var carts = await _context.Carts.Where<Carts>(cart => cart.UserId == userId
            && !cart.IsOrderPlaced).ToListAsync();
            if (carts == null || carts.Count == 0)
            {
                return NotFound("{\"message\" :\"You do not have carts.\"}");
            }
            return carts;
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<List<Carts>>> GetCartsOfOrder(int orderId)
        {
            var carts = await _context.Carts.Where<Carts>(cart => cart.OrderId == orderId
            && cart.IsOrderPlaced).ToListAsync();
            if (carts == null || carts.Count == 0)
            {
                return NotFound("{\"message\" :\"No carts with this cart id\"}");
            }
            return carts;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarts(int id, Carts carts)
        {
            if (id != carts.Id)
            {
                return BadRequest();
            }

            _context.Entry(carts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Carts>> PostCarts(Carts carts)
        {
            _context.Carts.Add(carts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarts", new { id = carts.Id }, carts);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Carts>> DeleteCarts(int id)
        {
            var carts = await _context.Carts.FindAsync(id);
            if (carts == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(carts);
            await _context.SaveChangesAsync();

            return carts;
        }

        private bool CartsExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
