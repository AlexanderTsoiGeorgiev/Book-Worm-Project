namespace BookWorm.Web.ViewModels.Poem
{
    public class PoemDisplayViewModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
    }
}
