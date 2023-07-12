namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BookPoem
    {
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [ForeignKey(nameof(Poem))]
        public Guid PoemId { get; set; }
        public Poem Poem { get; set; } = null!;
    }
}
