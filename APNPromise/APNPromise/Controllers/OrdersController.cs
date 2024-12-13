using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APNPromise.Models;
using Microsoft.AspNetCore.Authorization;

namespace APNPromise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersContext _context;

        public OrdersController(OrdersContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersList()
        {
            return await _context.OrdersList.Include(x => x.OrderLines).AsNoTracking().ToListAsync();
        }
    }
}
