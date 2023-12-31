﻿namespace BookWorm.Data.Models
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
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

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

        [Required]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        [Required]
        public ApplicationUser Author { get; set; } = null!;

        [ForeignKey(nameof(Poem))]
        public Guid? PoemId { get; set; }
        public Poem? Poem { get; set; }

        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; }
        public Book? Book { get; set; }
    }
}
