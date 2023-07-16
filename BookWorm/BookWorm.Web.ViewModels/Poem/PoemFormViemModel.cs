namespace BookWorm.Web.ViewModels.Poem
{
    using System.ComponentModel.DataAnnotations;

    using static BookWorm.Data.Common.DataModelsValidationConstants.PoemValidationConstants;

    public class PoemFormViemModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLenght, MinimumLength = ContentMinLenght)]
        public string Content { get; set; } = null!;

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
