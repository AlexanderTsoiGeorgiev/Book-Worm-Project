namespace BookWorm.Web.ViewModels.Reply
{
    using System.ComponentModel.DataAnnotations;
    using static BookWorm.Data.Common.DataModelsValidationConstants.ReplyValidationConstants;

    public class ReplyFormViewModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;

        public string? PostId { get; set; } = null!;
    }
}
