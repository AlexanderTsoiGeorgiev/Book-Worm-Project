namespace BookWorm.Services.Models.Poem
{
    using BookWorm.Web.ViewModels.Poem;

    public class PoemAllFilteredServiceModel
    {

        public PoemAllFilteredServiceModel()
        {
            Poems = new HashSet<PoemDisplayViewModel>();
        }

        public IEnumerable<PoemDisplayViewModel> Poems { get; set; }

        public int AllPoemsCount { get; set; }

    }
}
