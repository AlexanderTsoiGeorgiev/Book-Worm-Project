namespace BookWorm.Web.ViewModels.Reply
{
    public class ReplyDisplayViewModel
    {
        public string ReplyId { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string ForumPostId { get; set; } = null!;

        public string AuthorId { get; set; } = null!;

        public string AuthorName { get; set; } = null!;

    }
}
