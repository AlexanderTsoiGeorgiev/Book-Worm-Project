namespace BookWorm.Web.ViewModels.Poem
{
    using BookWorm.Web.ViewModels.Review;

    public class PoemReadViewModel
    {
        public PoemReadViewModel()
        {
            Reviews = new HashSet<ReviewDisplayViewModel>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public Guid AuthorId { get; set; }  

        public string AuthorName { get; set; } = null!;

        public string[] ReadableContent { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }


        public IEnumerable<ReviewDisplayViewModel>? Reviews { get; set; }
    }
}
