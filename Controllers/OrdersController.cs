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
    public class OrdersController : ControllerBase
    {
        private readonly APIDatabaseContext _context;

        public OrdersController(APIDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }
 
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Orders>>> GetUserOrders(int userId)
        {
            var orders = await _context.Orders.Where<Orders>(order => order.Userid == userId).ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound("{\"message\" :\"You do not have any orders.\"}");
            }

            return orders;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, Orders orders)
        {
            if (id != orders.Id)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(OrderCartModel orderCartModel)
        {
            var cartId = orderCartModel.CartId;
            var order = orderCartModel.Order;

            var cart = await _context.Carts.FindAsync(cartId);
            if(cart == null) {
                return NotFound("{\"message\" :\"You have sent the wrong cart Id.\"}");
            }
            if(cart.IsOrderPlaced) {
                return NotFound("{\"message\" :\"An order has already been placed for this cart.\"}");
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            cart.IsOrderPlaced = true;
            cart.OrderId = order.Id;
            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtAction("GetOrders", new { id = order.Id }, order);
        }

        [HttpPost("save-cart/")]
        public async Task<ActionResult<Orders>> SaveCartAndPostOrders(OrderCartModel orderCartModel)
        {
            var cart = orderCartModel.Cart;
            var order = orderCartModel.Order;
            
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            cart.IsOrderPlaced = true;
            cart.OrderId = order.Id;
            
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return orders;
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
