namespace BookWorm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Tag;
    using BookWorm.Services.Interfaces;
    using BookWorm.Data.Migrations;

    public class TagService : ITagService
    {
        private readonly BookWormDbContext dbContext;

        public TagService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Add
        public async Task AddTagAsync(TagFormViewModel model)
        {
            Tag tag = new Tag()
            {
                Name = model.Name
            };

            await dbContext.AddAsync(tag);
            await dbContext.SaveChangesAsync();
        }

        //All
        public async Task<IEnumerable<TagDisplayViewModel>> AllTagsAsDisplayModelAsync()
        {
            TagDisplayViewModel[] tags = await dbContext.Tags
                .AsNoTracking()
                .Select(t => new TagDisplayViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    isDeleted = t.isDeleted
                }).ToArrayAsync();

            return tags;
        }

        public async Task<IEnumerable<TagDisplayViewModel>> NonDeletedTagsAsDisplayModelAsync()
        {
            TagDisplayViewModel[] tags = await dbContext.Tags
                .AsNoTracking()
                .Where(t => t.isDeleted == false)
                .Select(t => new TagDisplayViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    isDeleted = t.isDeleted
                }).ToArrayAsync();

            return tags;
        }


        //Delete
        public async Task SoftDeleteAsync(int id)
        {
            Tag? tag = await dbContext.Tags.FindAsync(id);
            tag!.isDeleted = true;
            await dbContext.SaveChangesAsync();
        }

        //Restore
        public async Task RestoreAsync(int id)
        {
            Tag? tag = await dbContext.Tags.FindAsync(id);
            tag!.isDeleted = false;
            await dbContext.SaveChangesAsync();
        }

        //Validation
        public async Task<bool> TagExistsByNameAsync(string name)
        {
            Tag? tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.Name == name);
            return tag != null;
        }

        public async Task<bool> TagExistsByIdAsync(int id)
        {
            Tag? tag = await dbContext.Tags.FindAsync(id);
            return tag != null;
        }

        public async Task<bool> IsTagDeleted(int id)
        {
            Tag? tag = await dbContext.Tags.FindAsync(id);
            return tag!.isDeleted;
        }

        //Utility
        public async Task<IEnumerable<string>> AllTagNamesAsync()
        {
            IEnumerable<string> tags = await dbContext.Tags
                .AsNoTracking()
                .Where(t => !t.isDeleted)
                .Select(t => t.Name)
                .ToArrayAsync();

            return tags;
        }
    }
}
