namespace BookWorm.Services
{
    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Category;

    public class CategoryService : ICategoryService
    {
        private readonly BookWormDbContext dbContext;

        public CategoryService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //All
        public Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsDisplayModelAsyn()
        {
            throw new NotImplementedException();
        }

        //Add
        public async Task AddCategoryAsync(CategoryFormViewModel model)
        {
            Category category = new Category()
            {
                Name = model.Name
            };

            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        //Validation
        public async Task<bool> CategoryExistsAsync(CategoryFormViewModel model)
        {
            string name = model.Name.ToLower();

            return await dbContext.Categories.AnyAsync(c => c.Name.ToLower() == name);
        }
    }
}
