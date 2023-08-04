namespace BookWorm.Services
{
    using BookWorm.Data;
    using BookWorm.Services.Interfaces;

    public class ArticleService : IArticleService
    {
        private readonly BookWormDbContext dbContext;

        public ArticleService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
