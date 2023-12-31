﻿namespace BookWorm.Services.Interfaces
{
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Article;

    public interface IArticleService
    {
        //Add
        public Task CreateArticleAsync(string authorId, ArticleFormViewModel model);

        //Edit
        public Task EditArticleAsync(string id, ArticleFormViewModel model);
        public Task<Guid> GetArticlePoemIdAsync(string id);

        //Delete
        public Task SoftDeleteAsync(string id);

        //Validation
        public Task<bool> ExistsByIdAsync(string id);
        public Task<bool> IsUserArticleOwner(string authorId, string articleId);
        public Task<bool> IsDeletedAsync(string id);

        //Utility
        public Task<Article> FindArticleByIdAsync(string id);
        public Task<ArticleFormViewModel> FindArticleAsArticleFormViewModelByIdAsync(string id);
        public Task<ArticleReadViewModel> FindArticleAsArticleReadViewModelByIdAsync(string id);
        public Task<ArticleDetailsViewModel> FindArticleAsArticleDetailsViewModelByIdAsync(string id);
        public Task<IEnumerable<ArticleDisplayViewModel>> GetAllUserArticlesAsync(string authorId);
    }
}
