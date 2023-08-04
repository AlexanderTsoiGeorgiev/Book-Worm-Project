namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Article
    {
        public Article()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Poem))]
        public Guid PoemId { get; set; }
        [Required]
        public Poem Poem { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        [Required]
        public ApplicationUser Author { get; set; } = null!;
    }
}
