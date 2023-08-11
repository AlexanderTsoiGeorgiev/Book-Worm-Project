namespace BookWorm.Web.ViewModels.Forum
{
    using BookWorm.Web.ViewModels.Tag;
    using System.ComponentModel.DataAnnotations;
    using static BookWorm.Data.Common.DataModelsValidationConstants.ForumPostValidationConstants;

    public class ForumFormViewModel
    {
        public ForumFormViewModel()
        {
            Tags = new HashSet<TagDisplayViewModel>();
        }
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        [Display(Name = "Tag type")]
        public int TagId { get; set; }

        public IEnumerable<TagDisplayViewModel> Tags { get; set; }

    }
}
