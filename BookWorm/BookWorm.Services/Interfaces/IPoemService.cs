namespace BookWorm.Services.Interfaces
{
    using System.Threading.Tasks;

    using BookWorm.Web.ViewModels.Poem;

    public interface IPoemService
    {
        public Task<IEnumerable<PoemDisplayViewModel>> GetAllPoemsAsync();
        public Task CreatePoemAsync(PoemFormViemModel model);
        public Task EditPoemAsync(string id, PoemFormViemModel model);
    }
}
