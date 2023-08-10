namespace BookWorm.Services
{
    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Category;
    using System.Threading.Tasks;

    public class AdminService : IAdminService
    {
        private readonly BookWormDbContext dbContext;

        public AdminService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddCategoryAsync(CategoryFormViewModel model)
        {
            Category category = new Category()
            {
                Name = model.Name
            };

            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }
    }
}
