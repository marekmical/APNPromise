using Microsoft.EntityFrameworkCore;

namespace APNPromise.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var book = modelBuilder.Entity<Book>();
            book.HasKey(book => book.Id);
            book.HasMany(book => book.Authors);

            var author = modelBuilder.Entity<Author>();
            author.HasKey(author => new { author.FirstName, author.LastName });
        }

        public DbSet<Book> BooksList { get; set; } = null!;
    }
}
