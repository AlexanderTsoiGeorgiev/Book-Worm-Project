namespace BookWorm.Services.Interfaces
{
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Article;
    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.ViewModels.Review;
    using BookWorm.Web.ViewModels.User;

    public interface IUserService
    {
        //Admin below
        //All
        public Task<IEnumerable<UserAdminDisplayViewModel>> GetAllUsersDisplayViewModelAsync();

        //Create
        public Task CreateModeratorAsync(string userName);


        //User Below

        //Load Poems
        public Task<IEnumerable<PoemDisplayViewModel>> GetUserPoemsAsDisplayModelAsync(string id);

        //Load Books
        public Task<IEnumerable<BookDisplayViewModel>> GetUserBooksAsDisplayModelAsync(string id);

        //Load Reviews
        public Task<IEnumerable<ReviewDisplayViewModel>> GetUserReviewsAsDisplayModelAsync(string id);

        //Load Articles
        public Task<IEnumerable<ArticleDisplayViewModel>> GetUserArticlesAsDisplayModelAsync(string id);

        //Get User
        public Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        public Task<ApplicationUser> GetUserByIdAsync(string id);
    }
}
