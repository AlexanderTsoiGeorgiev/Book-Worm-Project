namespace BookWorm.Services.Interfaces
{
    using System.Threading.Tasks;

    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.ViewModels.Category;

    public interface IPoemService
    {
        public Task<IEnumerable<PoemDisplayViewModel>> GetAllUserPoemsAsync(string id);
        public Task CreatePoemAsync(PoemFormViemModel model);
        public Task EditPoemAsync(string id, PoemFormViemModel model);
        public Task SoftDeletePoemAsync(string id);

        //Note: This may require its own service
        public Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsync();
    }
}
