namespace BookWorm.Web.ViewModels.Forum
{
    public class ForumDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public string TagName { get; set; } = null!;
    }
}
