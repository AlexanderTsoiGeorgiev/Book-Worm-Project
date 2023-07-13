namespace BookWorm.Services
{
    using BookWorm.Data;
    using BookWorm.Services.Interfaces;

    public class BookService : IBookService
    {
        private readonly BookWormDbContext dbContext;

        public BookService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
