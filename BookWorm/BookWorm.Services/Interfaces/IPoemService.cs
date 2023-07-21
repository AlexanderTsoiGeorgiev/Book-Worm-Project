namespace BookWorm.Services.Interfaces
{
    using System.Threading.Tasks;

    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.ViewModels.Category;
    using BookWorm.Services.Models.Poem;

    public interface IPoemService
    {
        //All action
        public Task<IEnumerable<PoemDisplayViewModel>> GetAllPoemsAsync();
        public Task<PoemAllFilteredServiceModel> GetAllPoemsFilteredAsync(PoemQueryViewModel query);

        //Mine action
        public Task<IEnumerable<PoemDisplayViewModel>> GetAllUserPoemsAsync(string id);

        //Add action
        public Task CreatePoemAsync(string authorId, PoemFormViemModel model);

        //Edit action
        public Task<PoemFormViemModel> FindPoemByIdAsync(string id);
        public Task EditPoemAsync(string id, PoemFormViemModel model);

        public Task SoftDeletePoemAsync(string id);

        //Note: This may require its own service
        public Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsync();
        public Task<IEnumerable<string>> GetAllCategoryNamesAsync();
    }
}
