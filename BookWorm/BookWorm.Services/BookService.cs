namespace BookWorm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Web.ViewModels.Poem;

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
            Book entity = new Book
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Quantity = model.Quantity,
                AuthorId = Guid.Parse(authorId)
            };

            string[]? poemIds = model.PoemIds.Distinct().ToArray();

            ICollection<BookPoem> mappingEntites = new HashSet<BookPoem>();
            foreach (string poemId in model.PoemIds)
            {
                mappingEntites.Add(new BookPoem { BookId = entity.Id, PoemId = Guid.Parse(poemId) });
            }
            entity.BooksPoems = mappingEntites.ToArray();

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<BookFormViewModel> FindBookByIdFormModelAsync(int id)
        {
            BookFormViewModel? book = await dbContext.Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BookFormViewModel
                {
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Quantity = b.Quantity,
                })
                .FirstAsync();

            return book;
        }
        public async Task EditBookAsync(int id, BookFormViewModel model)
        {
            Book entity = await dbContext.Books.Include(b => b.BooksPoems).Where(b => b.Id == id).FirstAsync();
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.ImageUrl = model.ImageUrl;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;

            ICollection<BookPoem> mappingEntites = new HashSet<BookPoem>();
            foreach (string poemId in model.PoemIds)
            {
                mappingEntites.Add(new BookPoem { BookId = entity.Id, PoemId = Guid.Parse(poemId) });
            }
            //error occurs here probablly
            entity.BooksPoems = mappingEntites.ToList();

            await dbContext.SaveChangesAsync();
        }


        //TODO: FIX THIS METHOD!
        //idk what to fix
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

        public async Task<Book> FindBookByIdAsync(int id)
        {
            Book? entity = await dbContext.Books.FindAsync(id);

            return entity!;
        }

        public async Task SoftDeleteBookAsync(int id)
        {
            Book entity = await FindBookByIdAsync(id);

            entity.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }


        //Validation
        public async Task<bool> IsUserOwnerAsync(string userId, int bookId)
        {
            Book? book = await dbContext.Books.FindAsync(bookId);
            return book!.AuthorId.ToString() == userId;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool exists = await dbContext.Books.AnyAsync(b => b.Id == id); 
            return exists;
        }

        public async Task<bool> DoesUserOwnAllPoemsAsync(string userId, string[] poemIds)
        {
            ApplicationUser? user = await dbContext.Users.FindAsync(userId);
            ICollection<bool> allOwned = new List<bool>();
            foreach (string poemId in poemIds)
            {
                allOwned.Add(user!.Poems.Any(p => p.Id == Guid.Parse(poemId)));
            }

            return allOwned.All(b => b == true);
        }

        public async Task<IEnumerable<string>> GetSelectedPoemIdsAsync(int id)
        {
            string[] poems = await dbContext.BookPoem
               .Include(bp => bp.Poem)
               .Where(bp => bp.BookId == id)
               .Select(bp => bp.Poem.Id.ToString())
               .ToArrayAsync();

            return poems;
        }
    }
}
