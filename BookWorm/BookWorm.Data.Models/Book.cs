namespace BookWorm.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static BookWorm.Data.Common.DataModelsValidationConstants.BookValidationConstants;

    //TODO: Add summaries & comments
    public class Book
    {
        public Book()
        {
            Reviews = new HashSet<Review>();
            BooksPoems = new HashSet<BookPoem>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]        
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLenght)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Precision(6, 2)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        [Required]
        public ApplicationUser Author { get; set; } = null!;

        public IEnumerable<Review> Reviews { get; set; } = null!;

        public IEnumerable<BookPoem> BooksPoems { get; set; } = null!;
    }
}
