namespace BookWorm.Services
{
    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Article;
    using System.Threading.Tasks;

    public class ArticleService : IArticleService
    {
        private readonly BookWormDbContext dbContext;

        public ArticleService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateArticleAsync(string authorId, ArticleFormViewModel model)
        {
            Article article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                AuthorId = Guid.Parse(authorId),
                PoemId = model.PoemId,

            };

            await dbContext.AddAsync(article);
            await dbContext.SaveChangesAsync();
        }
    }
}
