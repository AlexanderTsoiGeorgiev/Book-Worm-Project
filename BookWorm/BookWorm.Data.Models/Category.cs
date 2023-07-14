namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            Poems = new HashSet<Poem>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public IEnumerable<Poem> Poems { get; set; }
    }
}
