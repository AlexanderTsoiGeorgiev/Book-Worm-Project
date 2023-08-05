namespace BookWorm.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static BookWorm.Data.Common.DataModelsValidationConstants.TagValidationConstants;

    public class Tag
    {
        public Tag()
        {
            ForumPosts = new HashSet<ForumPost>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<ForumPost> ForumPosts { get; set; }
    }
}
