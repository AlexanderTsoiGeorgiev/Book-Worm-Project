namespace BookWorm.Services.Interfaces
{
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.ViewModels.Review;

    public interface IBookService
    {
        //Add
        public Task CreateBookAsync(string authorId, BookFormViewModel model);

        //Mine
        public Task<IEnumerable<BookDisplayViewModel>?> GetAllUserBooksAsync(string authorId);

        //Edit
        public Task EditBookAsync(int id, BookFormViewModel model);
        public Task<IEnumerable<string>> GetSelectedPoemIdsAsync(int id);

        //Read
        public Task<BookReadViewModel> GetBookAsBookReadModelAsync(int id);
        public Task<IList<PoemBookReadViewModel>> GetBookPoemsAsPoemBookReadModelAsync(int id);
        public Task<IEnumerable<ReviewDisplayViewModel>?> GetBookReviewsAsReviewDisplayViewModel(int id);

        //Delete
        public Task SoftDeleteBookAsync(int id);

        //Validation
        public Task<bool> ExistsByIdAsync(int id);
        public Task<bool> IsUserOwnerAsync(string userId, int bookId);
        public Task<bool> IsDeletedAsync(int id);
        public Task<bool> DoesUserOwnAllPoemsAsync(string userId, string[] poemIds);

        //Utility
        public Task<Book> FindBookByIdAsync(int id);
        public Task<BookFormViewModel> FindBookByIdFormModelAsync(int id);
        public Task<BookReadViewModel> FindBookByIdReadModelAsync(int id);
    }
}
