namespace BookWorm.Web.ViewModels.Article
{
    public class ArticleDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime DateCreated { get; set; } 
        public DateTime? DateEdited { get; set; } 
    }
}
