namespace BookWorm.Services.Interfaces
{
    using System.Threading.Tasks;

    using BookWorm.Web.ViewModels.Category;

    public interface ICategoryService
    {
        //All
        public Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsDisplayModelAsync();

        //Add
        public Task AddCategoryAsync(CategoryFormViewModel model);

        //Delete
        public Task SoftDeleteCategoryAsync(int id);

        //Restore
        public Task RestoreCategoryAsync(int id);

        //Validation
        public Task<bool> CategoryExistsByIdAsync(int id);
        public Task<bool> CategoryExistsByNameAsync(CategoryFormViewModel model);
    }
}
