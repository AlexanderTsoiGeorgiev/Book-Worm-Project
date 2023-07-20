namespace BookWorm.Web.ViewModels.Poem
{
    using BookWorm.Web.ViewModels.Poem.Enums;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class PoemQueryViewModel
    {
        public PoemQueryViewModel()
        {
            Poems = new HashSet<PoemDisplayViewModel>();
            Categories = new HashSet<string>();

            PoemsPerPage = PoemsPerPageDefault;
            CurrentPage = CurrentPageDefault;
        }

        public string? QueryString { get; set; }

        public string? CategoryName { get; set; } 

        public int PoemsCount { get; set;}

        public int CurrentPage { get; set; }

        public int PoemsPerPage { get; set; }

        public PoemSort OrderBy { get; set; }

        public IEnumerable<PoemDisplayViewModel> Poems { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
