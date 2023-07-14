namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static BookWorm.Data.Common.DataModelsValidationConstants.PoemValidationConstants;

    //TODO: Add comments & summaries
    public class Poem
    {
        public Poem()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ContentMaxLenght)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; } = null!;

        public IEnumerable<Review> Reviews { get; set; } = null!;

        public IEnumerable<BookPoem> BooksPoems { get; set; } = null!;
    }
}
