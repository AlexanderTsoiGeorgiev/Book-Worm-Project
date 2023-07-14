namespace BookWorm.Web.ViewModels.Review
{
    //TODO: Check if validation interferes with the display model
    public class ReviewDisplayViewModel : ReviewFormViewModel
    {
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateEdited { get; set; }
        public string? BookName { get; set; }
        public string? PoemName { get; set; }
    }
}
