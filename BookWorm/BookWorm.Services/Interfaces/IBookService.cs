namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Book;

    public interface IBookService
    {
        public Task<IEnumerable<BookDisplayViewModel>> GetAllUserBooksAsync(string authorId);
        public Task CreateBookAsync(BookFormViewModel model);
    }
}
