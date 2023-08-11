namespace BookWorm.Web.ViewModels.Tag
{
    public class TagDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool isDeleted { get; set; }
    }
}
