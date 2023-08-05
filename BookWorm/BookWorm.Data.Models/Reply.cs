//namespace BookWorm.Data.Models
//{
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;

//    using static BookWorm.Data.Common.DataModelsValidationConstants.ReplyValidationConstants;

//    public class Reply
//    {
//        public Reply()
//        {
//            Id = Guid.NewGuid();    
//        }

//        [Key]
//        public Guid Id { get; set; }

//        [Required]
//        [MaxLength(ContentMaxLength)]
//        public string Content { get; set; } = null!;

//        [Required]
//        [ForeignKey(nameof(Author))]
//        public Guid AuthorId { get; set; }
//        [Required]
//        public ApplicationUser Author { get; set; } = null!;

//        [Required]
//        [ForeignKey(nameof(ForumPost))]
//        public Guid ForumPostId { get; set; }
//        [Required]
//        public ForumPost ForumPost { get; set; } = null!;
//    }
//}
