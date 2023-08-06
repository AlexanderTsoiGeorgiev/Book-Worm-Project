namespace BookWorm.Web.ViewModels.Review
{
    public class ReviewDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string? PoemId { get; set; }
        public int? BookId { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public float Rating { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }
    }
}
