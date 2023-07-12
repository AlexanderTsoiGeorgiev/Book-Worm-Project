namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BookPoem
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        [ForeignKey(nameof(Poem))]
        public string PoemId { get; set; } = null!;
        public Poem Poem { get; set; } = null!;
    }
}
