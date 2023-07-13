namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Data.Models;


    //TODO: Add exceptions and add summaries
    public class PoemService : IPoemService
    {
        private readonly BookWormDbContext dbContext;

        public PoemService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

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

        public async Task<IEnumerable<PoemDisplayViewModel>> GetAllPoemsAsync()
        {
            var allPoems = await dbContext.Poems
                .Where(p => p.IsDeleted == false)
                .Select(p => new PoemDisplayViewModel
                {
                    Title= p.Title,
                    DateCreated = p.DateCreated,
                    Description = p.Description
                })
                .ToArrayAsync();
            return allPoems;
        }
    }
}
