namespace BookWorm.Services.Interfaces
{
    using System.Threading.Tasks;

    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Services.Models.Poem;
    using BookWorm.Web.ViewModels.Category;

    public interface IPoemService
    {
        //All action
        public Task<IEnumerable<PoemDisplayViewModel>> GetAllPoemsAsync();
        public Task<PoemAllFilteredServiceModel> GetAllPoemsFilteredAsync(PoemQueryViewModel query);

        //Read action
        public Task<PoemReadViewModel> FindPoemReadModelByIdAsync(string id);

        //Mine action
        public Task<IEnumerable<PoemDisplayViewModel>?> GetAllUserPoemsAsync(string id);

        //Add action
        public Task CreatePoemAsync(string authorId, PoemFormViemModel model);

        //Edit action
        public Task<PoemFormViemModel> FindPoemByIdAsync(string id);  // this one may not be needed
        public Task EditPoemAsync(string id, PoemFormViemModel model);

        //Details
        public Task<PoemDetailsVisualizeViewModel?> GetPoemAsDetailsViewModelByIdAsync(string id);

        //Delete action
        public Task SoftDeletePoemAsync(string id);

        //Validation
        public Task<bool> IsPoemPrivateAsync(string id);
        public Task<bool> ExistsByIdAsync(string id);
        public Task<bool> IsUserPoemOwnerAsync(string userId, string poemId);
        public Task<bool> IsPoemDeletedAsync(string id);

        //Note: This may require its own service
        public Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsync();
        public Task<IEnumerable<string>> GetAllCategoryNamesAsync();

        //Used by BookService
        public Task<IEnumerable<PoemBookSelectViewModel>> GetUserPoemsAsPoemBookSelectViewModelAsync(string userId);
        //this one may need its own UserService
        public Task<bool> UserHasPoemsAsync(string id);
    }
}
