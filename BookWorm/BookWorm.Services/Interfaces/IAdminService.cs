namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Category;

    public interface IAdminService
    {
        public Task AddCategoryAsync(CategoryFormViewModel model);
    }
}
