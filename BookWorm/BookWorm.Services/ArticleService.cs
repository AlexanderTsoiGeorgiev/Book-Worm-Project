namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Article;

    public class ArticleService : IArticleService
    {
        private readonly BookWormDbContext dbContext;

        public ArticleService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Add
        public async Task CreateArticleAsync(string authorId, ArticleFormViewModel model)
        {
            Article article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                AuthorId = Guid.Parse(authorId),
                PoemId = model.PoemId,
                DatePosted = DateTime.Now
            };

            await dbContext.AddAsync(article);
            await dbContext.SaveChangesAsync();
        }

        //Edit
        public async Task EditArticleAsync(string id, ArticleFormViewModel model)
        {
            Article article = await FindArticleByIdAsync(id);
            article.Title = model.Title;
            article.Content = model.Content;
            article.DateEdited = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }
        public async Task<Guid> GetArticlePoemIdAsync(string id)
        {
            Article article = await FindArticleByIdAsync(id);
            return article.PoemId;
        }


        //Validation
        public async Task<bool> ExistsByIdAsync(string id)
        {
            Article? article = await dbContext.Articles.FindAsync(Guid.Parse(id));
            return article != null;
        }
        public async Task<bool> IsUserArticleOwner(string authorId, string articleId)
        {
            Article article = await FindArticleByIdAsync(articleId);
            return article.AuthorId == Guid.Parse(authorId);
        }
        public async Task<bool> IsDeletedAsync(string id)
        {
            Article article = await FindArticleByIdAsync(id);
            return article.IsDeleted;
        }


        //Utility
        public async Task<Article> FindArticleByIdAsync(string id)
        {
            Article? article = await dbContext.Articles.FindAsync(Guid.Parse(id));

            return article!;
        }
        public async Task<ArticleFormViewModel> FindArticleAsArticleFormViewModelByIdAsync(string id)
        {
            ArticleFormViewModel model = await dbContext.Articles
                .Where(a => a.Id == Guid.Parse(id))
                .Select(a => new ArticleFormViewModel
                {
                    Title = a.Title,
                    Content = a.Content,
                    PoemId = a.PoemId,
                }).FirstAsync();

            return model;
        }

      
    }
}
