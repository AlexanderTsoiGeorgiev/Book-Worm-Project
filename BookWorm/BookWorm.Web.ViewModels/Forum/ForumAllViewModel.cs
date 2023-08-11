namespace BookWorm.Web.ViewModels.Forum
{

    public class ForumAllViewModel
    {
        public ForumAllViewModel()
        {
            Tags = new HashSet<string>();
        }

        public IEnumerable<string> Tags { get; set; }

        public string QueryString { get; set; } = null!;
    }
}
