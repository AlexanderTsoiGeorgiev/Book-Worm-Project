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
        public async Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsDisplayModelAsync()
        {
            CategoryDisplayViewModel[] categories = await dbContext.Categories.AsNoTracking().Select(c => new CategoryDisplayViewModel
            {
                Id = c.Id,
                Name = c.Name,
                isDeleted = c.IsDeleted
            }).ToArrayAsync();

            return categories;
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

        //Delete
        public async Task SoftDeleteCategoryAsync(int id)
        {
            Category? category = await dbContext.Categories.FindAsync(id);
            category!.IsDeleted = true;
            await dbContext.SaveChangesAsync();
        }

        //Restore
        public async Task RestoreCategoryAsync(int id)
        {
            Category? category = await dbContext.Categories.FindAsync(id);
            category!.IsDeleted = false;
            await dbContext.SaveChangesAsync();
        }

        //Validation
        public async Task<bool> CategoryExistsByNameAsync(CategoryFormViewModel model)
        {
            string name = model.Name.ToLower();

            bool result = await dbContext.Categories.AnyAsync(c => c.Name.ToLower() == name);
            return result;
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            Category? category = await dbContext.Categories.FindAsync(id);
            return category != null;
        }
    }
}
