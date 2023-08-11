namespace BookWorm.Web.ViewModels.Article
{
    public class ArticleDisplayViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string PoemId { get; set; } = null!;
    }
}
