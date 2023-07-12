namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Book
    {

        [Key]
        public int Id { get; set; }

        [Required]

        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        //This one may not be necessary
        [Required]
        public IEnumerable<Poem> Contents { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
