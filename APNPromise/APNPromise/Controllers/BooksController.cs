using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APNPromise.Models;

namespace APNPromise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksList()
        {
            return await _context.BooksList.Include(x => x.Authors).ToListAsync();
        }


        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            try
            {
                _context.BooksList.Add(book);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentException argEx)
            {
                return new CreatedResult("Book with given ID already exists", 400);
            }

            return Created();
        }
    }
}
