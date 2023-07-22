namespace BookWorm.Web.ViewModels.Poem
{
    public class PoemDetailsVisualizeViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string[] ReadableContent { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public bool IsPrivate { get; set; }
    }
}
