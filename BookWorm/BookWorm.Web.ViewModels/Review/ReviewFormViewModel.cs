namespace BookWorm.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;

    using static BookWorm.Data.Common.DataModelsValidationConstants.ReviewValidationConstants;

    //TODO: Check if validation interferes with the display model
    public class ReviewFormViewModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        [Range(MinRating, MaxRating)]
        public float Rating { get; set; }

        public Guid? PoemId { get; set; }

        public int? BookId { get; set; }
    }
}
