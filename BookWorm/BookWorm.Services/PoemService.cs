namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Category;


    //TODO: Add exceptions and add summaries
    public class PoemService : IPoemService
    {
        private readonly BookWormDbContext dbContext;

        public PoemService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Check TODOs
        public async Task CreatePoemAsync(PoemFormViemModel model)
        {
            var entity = new Poem
            {
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                IsPrivate = model.IsPrivate,
                DateCreated = DateTime.Now,
            };

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        //Check TODOs
        public async Task EditPoemAsync(string id, PoemFormViemModel model)
        {
            var entity = await dbContext.Poems.FindAsync(id);
            if (entity == null)
            {
                return; //Add exception
            }
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.Description = model.Description;
            entity.IsPrivate = model.IsPrivate;
            entity.DateEdited = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsync()
        {
            CategoryDisplayViewModel[] categories = await dbContext.Categories
                .AsNoTracking()
                .Select(c => new CategoryDisplayViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return categories;
        }

        //Check TODOs
        public async Task<IEnumerable<PoemDisplayViewModel>> GetAllPoemsAsync()
        {
            var allPoems = await dbContext.Poems
                .Where(p => p.IsDeleted == false)
                .Select(p => new PoemDisplayViewModel
                {
                    Title = p.Title,
                    DateCreated = p.DateCreated,
                    Description = p.Description
                })
                .AsNoTracking()
                .ToArrayAsync();
            return allPoems;
        }

        //Check TODOs
        public async Task SoftDeletePoemAsync(string id)
        {
            var entity = await dbContext.Poems.FindAsync(id);
            if (entity == null)
            {
                return; //Add exception
            }
            entity.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }
    }
}
