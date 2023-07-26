namespace BookWorm.Services.Interfaces
{
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Book;

    public interface IBookService
    {
        public Task<IEnumerable<BookDisplayViewModel>> GetAllUserBooksAsync(string authorId);
        public Task CreateBookAsync(string authorId, BookFormViewModel model);

        public Task<BookFormViewModel> FindBookByIdFormModelAsync(int id);
        public Task<Book> FindBookByIdAsync(int id);
        public Task EditBookAsync(int id, BookFormViewModel model);


        public Task SoftDeleteBookAsync(int id);
        public Task<bool> IsUserOwnerAsync(string userId);

        //Validation
        public Task<bool> DoesUserOwnAllPoemsAsync(string userId, string[] poemIds);
    }
}
