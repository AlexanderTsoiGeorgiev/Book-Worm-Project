namespace BookWorm.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            Poems = new HashSet<Poem>();
            Books = new HashSet<Book>();
            Reviews = new HashSet<Review>();
            Replies = new HashSet<Reply>();
        }
        public ICollection<Poem> Poems { get; set; } = null!;
        public ICollection<Book> Books { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<Reply> Replies { get; set; } = null!;
    }
}
