namespace BookWorm.Web.ViewModels.Poem
{
    public class PoemDisplayViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
    }
}
