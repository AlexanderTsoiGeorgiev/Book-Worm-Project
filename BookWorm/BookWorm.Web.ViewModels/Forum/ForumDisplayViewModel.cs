namespace BookWorm.Web.ViewModels.Forum
{
    public class ForumDisplayViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public DateTime DatePosted { get; set; }
    }
}
