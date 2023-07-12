namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static BookWorm.Data.Common.DataModelsValidationConstants.ReviewValidationConstants;

    //TODO: Add comments, summaries and fix data types
    public class Review
    {
        public Review()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public float Rating { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        public DateTime? DateEdited { get; set; }

        [Required]
        public int Upvotes { get; set; }

        [Required]
        public int Downvotes { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        //[ForeignKey(nameof(Poem))]
        //public string PoemId { get; set; } = null!;
        //public Poem Poem { get; set; } = null!;

        //[ForeignKey(nameof(Book))]
        //public int BookId { get; set; }
        //public Book Book { get; set; } = null!;
    }
}
