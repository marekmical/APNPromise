using APNPromise.Controllers;
using APNPromise.Models;
using Microsoft.EntityFrameworkCore;

namespace APNPromiseTests
{
    public class BooksTests
    {

        [Test]
        public async Task GetBooksList_ShouldReturnCorrectBookCount()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(databaseName: "Books Test")
                .Options;

            var testBooks = GetTestBooks();
            using (var booksContext = new BooksContext(options))
            {
                booksContext.AddRange(testBooks);
                booksContext.SaveChanges();
            }

            using (var booksContext = new BooksContext(options))
            {
                var controller = new BooksController(booksContext);

                var result = await controller.GetBooksList();
                Assert.That(result.Value, Is.Not.Null);
                Assert.That(result.Value.Count, Is.EqualTo(testBooks.Count));
                booksContext.Database.EnsureDeleted();
            }
        }

        [Test]
        public async Task PostBook_ShouldAddCorrectBook()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(databaseName: "Books Test")
                .Options;

            var testBooks = GetTestBooks();
            using (var booksContext = new BooksContext(options))
            {
                var controller = new BooksController(booksContext);
                var postResult = await controller.PostBook(testBooks.First());
                var getResult = await controller.GetBooksList();

                Assert.That(getResult.Value, Is.Not.Null);
                Assert.That(getResult.Value.First, Is.EqualTo(testBooks.First()));
                booksContext.Database.EnsureDeleted();
            }
        }

        private List<Book> GetTestBooks()
        {
            var testBooks = new List<Book>();
            testBooks.Add(new Book { Id = 1, Title = "Title1", Price = 1, Bookstand = 1, Shelf = 10, Authors = new[] { new Author { FirstName = "FirstName1", LastName = "LastName1" } } });
            testBooks.Add(new Book { Id = 2, Title = "Title2", Price = 100, Bookstand = 2, Shelf = 20, Authors = new[] { new Author { FirstName = "FirstName2", LastName = "LastName2" } } });
            testBooks.Add(new Book { Id = 3, Title = "Title2", Price = 10000, Bookstand = 3, Shelf = 30, Authors = new[] { new Author { FirstName = "FirstName3", LastName = "LastName3" } } });
            testBooks.Add(new Book { Id = 4, Title = "Title2", Price = 10000000, Bookstand = 4, Shelf = 40, Authors = new[] { new Author { FirstName = "FirstName4", LastName = "LastName4" } } });

            return testBooks;
        }
    }
}