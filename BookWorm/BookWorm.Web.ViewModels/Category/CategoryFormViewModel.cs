namespace BookWorm.Web.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using static BookWorm.Data.Common.DataModelsValidationConstants.CategoryValidationConstants;

    public class CategoryFormViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
