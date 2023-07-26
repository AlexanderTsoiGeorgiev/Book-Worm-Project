namespace BookWorm.Web.ViewModels.Review
{
    //TODO: Check if validation interferes with the display model
    public class ReviewDisplayViewModel
    {
        public Guid Id { get; set; }
        public Guid? PoemId { get; set; }
        public int? BookId { get; set; }

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public float Rating { get; set; }

        public DateTime DatePosted { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
