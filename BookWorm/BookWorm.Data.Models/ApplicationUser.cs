namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using static BookWorm.Data.Common.DataModelsValidationConstants.UserValidationConstant;

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

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public ICollection<Poem> Poems { get; set; } = null!;
        public ICollection<Book> Books { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<Reply> Replies { get; set; } = null!;
    }
}
