namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Forum;
    using Microsoft.EntityFrameworkCore;

    public class ForumPostService : IForumPostService
    {
        private readonly BookWormDbContext dbContext;

        public ForumPostService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Add
        public async Task AddForumPostAsync(string authorId, ForumFormViewModel model)
        {
            ForumPost post = new ForumPost()
            {
                AuthorId = Guid.Parse(authorId),
                Title = model.Title,
                Content = model.Content,
                DatePosted = DateTime.Now,
                DateEdited = null,
                IsDeleted = false,
                TagId = model.TagId,
            };

            await dbContext.AddAsync(post);
            await dbContext.SaveChangesAsync();
        }

        //Edit
        public async Task EditForumPostAsync(string id, ForumFormViewModel model)
        {
            ForumPost? post = await dbContext.ForumPosts.FindAsync(Guid.Parse(id));

            post!.Title = model.Title;
            post!.Content = model.Content;
            post!.TagId = model.TagId;
            post!.DateEdited = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }

        //Delete
        public async Task SoftDeletePostAsync(string id)
        {
            ForumPost? post = await dbContext.ForumPosts.FindAsync(Guid.Parse(id));
            post!.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        //Validation
        public async Task<bool> ForumPostExistsAsync(string id)
        {
            ForumPost? post = await dbContext.ForumPosts.FindAsync(Guid.Parse(id));
            return post != null;
        }
        public async Task<bool> IsPostDeletedAsync(string id)
        {
            ForumPost? post = await dbContext.ForumPosts.FindAsync(Guid.Parse(id));
            return post!.IsDeleted;
        }
        public async Task<bool> IsUserOwnerAsync(string authorId, string postId)
        {
            ForumPost? post = await dbContext.ForumPosts.FindAsync(Guid.Parse(postId));

            return post!.AuthorId.ToString().ToLower() == authorId.ToLower();
        }

        public async Task<ForumFormViewModel> FindForumPostAsForumFormModelAsync(string id)
        {
            ForumPost entity = await dbContext.ForumPosts.FirstAsync(fp => fp.Id.ToString().ToLower() == id.ToLower());

            ForumFormViewModel model = new ForumFormViewModel
            {
                Title = entity.Title,
                Content = entity.Content,
                TagId = entity.TagId
            };

            return model;
            
        }
    }
}
