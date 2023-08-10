namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static BookWorm.Data.Common.DataModelsValidationConstants.CategoryValidationConstants;


    //TODO: Add comments and summaries
    public class Category
    {
        public Category()
        {
            Poems = new HashSet<Poem>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; }

        public IEnumerable<Poem> Poems { get; set; }
    }
}
