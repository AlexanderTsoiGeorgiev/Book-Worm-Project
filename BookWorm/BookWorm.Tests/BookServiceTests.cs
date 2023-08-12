namespace BookWorm.Tests
{
    using BookWorm.Data;
    using BookWorm.Services.Interfaces;
    using BookWorm.Services;
    using Microsoft.EntityFrameworkCore;
    using BookWorm.Data.Models;

    using static BookWorm.Data.Common.AuthorIds;

    public class BookServiceTests
    {
        private DbContextOptions<BookWormDbContext> dbOptions;
        private BookWormDbContext dbContext;

        private IBookService bookService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<BookWormDbContext>()
                .UseInMemoryDatabase("BookWormDbContext" + Guid.NewGuid().ToString())
                .Options;

            dbContext = new BookWormDbContext(dbOptions);

            dbContext.Database.EnsureCreated();
            SeedBooks();

            bookService = new BookService(dbContext);
        }

        [Test]
        public async Task TestExistsByValidId()
        {
            int validId = 1;

            bool result = await bookService.ExistsByIdAsync(validId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestExistsByInvalidId()
        {
            int invalidId = 0;

            bool result = await bookService.ExistsByIdAsync(invalidId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestIsUserOwner()
        {
            int bookId = 1;
            string userId = EdgarAllanPoeId;

            bool result = await bookService.IsUserOwnerAsync(userId.ToLower(), bookId);

            Assert.IsTrue(result);
        }

        private void SeedBooks() 
        {
            List<Book> books = new List<Book>();
            Book book;

            book = new Book
            {
                Title = "First Book",
                Description = "Description",
                Reviews = new List<Review>(),
                AuthorId = Guid.Parse(EdgarAllanPoeId),
                Quantity = 1,
                Price = 1,
            };
            books.Add(book);

            book = new Book
            {
                Title = "Second Book",
                Description = "Description",
                Reviews = new List<Review>(),
                AuthorId = Guid.Parse(EmilyDickinsonId),
                Quantity = 1,
                Price = 1,
            };
            books.Add(book);

            book = new Book
            {
                Title = "Third Book",
                Description = "Description",
                Reviews = new List<Review>(),
                AuthorId = Guid.Parse(WilliamShakespeareId),
                Quantity = 1,
                Price = 1,
            };
            books.Add(book);

            dbContext.AddRange(books);
            dbContext.SaveChanges();
        }
    }
}
