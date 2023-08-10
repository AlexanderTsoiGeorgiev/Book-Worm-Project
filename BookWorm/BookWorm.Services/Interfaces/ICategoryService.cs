namespace BookWorm.Services.Interfaces
{
    using System.Threading.Tasks;

    using BookWorm.Web.ViewModels.Category;

    public interface ICategoryService
    {
        //All
        public Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsDisplayModelAsyn();

        //Add
        public Task AddCategoryAsync(CategoryFormViewModel model);

        //Validation
        public Task<bool> CategoryExistsAsync(CategoryFormViewModel model);
    }
}
