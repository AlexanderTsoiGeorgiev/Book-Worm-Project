namespace BookWorm.Web.ViewModels.Book
{
    using System.ComponentModel.DataAnnotations;

    using static BookWorm.Data.Common.DataModelsValidationConstants.BookValidationConstants;

    //TODO: Add Categories, add error messages & determine if [Range] attribute works properly
    public class BookFormViewModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLenght, MinimumLength = ImageUrlMinLenght)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Required]
        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; }
    }
}
