namespace BookWorm.Tests
{
    using BookWorm.Data;
    using BookWorm.Services.Interfaces;
    using BookWorm.Services;
    using Microsoft.EntityFrameworkCore;
    using BookWorm.Data.Models;

    using static BookWorm.Data.Common.AuthorIds;
    using BookWorm.Web.ViewModels.Book;
    using System.Collections.ObjectModel;

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

        [Test]
        public async Task TestCreateBook()
        {
            string guid1 = Guid.NewGuid().ToString();
            string guid2 = Guid.NewGuid().ToString();
            string guid3 = Guid.NewGuid().ToString();
            int count = dbContext.Books.Count();
            count++;
            BookFormViewModel model = new BookFormViewModel()
            {
                Description = "Description",
                ImageUrl = "image url",
                Title = "Title",
                Quantity = 1,
                Price = 1,
                PoemIds = new Collection<string>() { guid1, guid2, guid3 }
            };

            string authorId = Guid.NewGuid().ToString();

            await bookService.CreateBookAsync(authorId, model);

            Assert.That(count == dbContext.Books.Count());
            Assert.IsNotNull(model);
            Assert.IsNotNull(dbContext.Books.Find(Guid.Parse(authorId)));
            Assert.IsNotNull(dbContext.Books.Find(Guid.Parse(authorId)).BooksPoems);

        }

        [Test]
        public async Task TestGetAllUserBooks()
        {
            string userId = EdgarAllanPoeId.ToString();

            var books = await bookService.GetAllUserBooksAsync(userId);

            Assert.NotNull(books);
        }

        [Test]
        public async Task TestEditBook()
        {
            int id = 1;
            string guid1 = Guid.NewGuid().ToString();
            Book book = dbContext.Books.Find(1);
            string titleBeforeChange = book.Title;
            BookFormViewModel model = new BookFormViewModel
            {
                Description = "Description",
                Title = "Titlte",
                ImageUrl = "img url",
                Price = 5,
                Quantity = 1,
                PoemIds = new Collection<string>() {guid1 },
            };

            await bookService.EditBookAsync(id, model);

            Assert.NotNull(model);
            Assert.That(titleBeforeChange != book.Title);
        }


        [Test]
        public async Task TestGetBookAsBookReadModel()
        {
            int bookdId = 1;

            var result = await bookService.GetBookAsBookReadModelAsync(bookdId);

            Assert.NotNull(result);
        }

        private void SeedBooks()
        {
            List<Book> books = new List<Book>();
            Book book;

            book = new Book
            {
                Id = 1,
                Title = "First Book",
                Description = "Description",
                Reviews = new List<Review>(),
                AuthorId = Guid.Parse(EdgarAllanPoeId),
                Quantity = 1,
                Price = 1,
                ImageUrl = "some url",
                Author = new ApplicationUser(),
                BooksPoems = new List<BookPoem>()
                {
                    new BookPoem(), new BookPoem()
                }
            };
            books.Add(book);

            book = new Book
            {
                Id = 2,
                Title = "Second Book",
                Description = "Description",
                Reviews = new List<Review>(),
                AuthorId = Guid.Parse(EmilyDickinsonId),
                Quantity = 1,
                Price = 1,
                ImageUrl = "somde url",
                Author = new ApplicationUser(),
                BooksPoems = new List<BookPoem>()
                {
                    new BookPoem(), new BookPoem()
                }
            };
            books.Add(book);

            book = new Book
            {
                Id = 3,
                Title = "Third Book",
                Description = "Description",
                Reviews = new List<Review>(),
                AuthorId = Guid.Parse(WilliamShakespeareId),
                Quantity = 1,
                Price = 1,
                ImageUrl = "some url",
                Author = new ApplicationUser(),
                BooksPoems = new List<BookPoem>()
                {
                    new BookPoem(), new BookPoem()
                }
            };
            books.Add(book);

            dbContext.AddRange(books);
        }
    }
}
