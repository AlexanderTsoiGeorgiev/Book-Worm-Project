namespace BookWorm.Web.ViewModels.Book
{
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.ViewModels.Review;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class BookReadViewModel
    {
        public BookReadViewModel()
        {
            Poems = new HashSet<PoemBookReadViewModel>();
            CurrentPage = CurrentPageDefault;
        }

        public string Title { get; set; } = null!; 
        public string AuthorUserName { get; set; } = null!; 
        public int CurrentPage { get; set; }
        public int PreviousPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<PoemBookReadViewModel> Poems { get; set; }
        public PoemBookReadViewModel CurrentPoem { get; set; } = null!;
        public IEnumerable<ReviewDisplayViewModel>? Reviews { get; set; }
    }
}
