﻿namespace BookWorm.Services.Interfaces
{
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Web.ViewModels.Poem;

    public interface IBookService
    {
        //Add
        public Task CreateBookAsync(string authorId, BookFormViewModel model);

        public Task<IEnumerable<BookDisplayViewModel>> GetAllUserBooksAsync(string authorId);

        //Edit
        public Task EditBookAsync(int id, BookFormViewModel model);
        public Task<IEnumerable<PoemBookSelectViewModel>> LoadPoemsIntoFromViewModelAsync(int id);
        public Task<BookFormViewModel?> FindBookByIdFormModelAsync(int id);

        //Delete
        public Task SoftDeleteBookAsync(int id);

        //Validation
        public Task<bool> ExistsByIdAsync(int id);
        public Task<Book> FindBookByIdAsync(int id);
        public Task<bool> IsUserOwnerAsync(string userId);
        public Task<bool> DoesUserOwnAllPoemsAsync(string userId, string[] poemIds);
    }
}
