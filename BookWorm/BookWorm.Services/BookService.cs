namespace BookWorm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Book;

    public class BookService : IBookService
    {
        private readonly BookWormDbContext dbContext;

        public BookService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //TODO: Add exeptions and Author id should be recieved as a parameter
        public async Task CreateBookAsync(string authorId, BookFormViewModel model)
        {
            var entity = new Book
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Quantity = model.Quantity,
                AuthorId = Guid.Parse(authorId)
            };

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        //TODO: FIX THIS METHOD!
        public async Task<IEnumerable<BookDisplayViewModel>> GetAllUserBooksAsync(string authorId)
        {
            var userName = dbContext.Users.Find(Guid.Parse(authorId))?.UserName ?? throw new ArgumentNullException();
            var userBooks = await dbContext.Books.Where(b => b.AuthorId.ToString() == authorId).Select(b => new BookDisplayViewModel
            {
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                AuthorName = userName
            }).ToArrayAsync();

            return userBooks!;
        }
    }
}
