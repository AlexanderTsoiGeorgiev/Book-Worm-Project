namespace BookWorm.Web.ViewModels.Forum
{

    public class ForumAllViewModel
    {
        public ForumAllViewModel()
        {
            Tags = new HashSet<string>();
            Forums = new HashSet<ForumDisplayViewModel>();
        }

        public string TagName { get; set; } = null!;

        public IEnumerable<string> Tags { get; set; }

        public string QueryString { get; set; } = null!;

        public IEnumerable<ForumDisplayViewModel> Forums { get; set; }
    }
}
