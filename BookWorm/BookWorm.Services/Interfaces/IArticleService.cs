namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Article;

    public interface IArticleService
    {
        public Task CreateArticleAsync(string authorId, ArticleFormViewModel model);
    }
}
