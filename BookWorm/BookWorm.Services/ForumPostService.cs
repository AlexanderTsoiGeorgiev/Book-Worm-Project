namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Forum;

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

        public async Task<IEnumerable<ForumDisplayViewModel>> SortedForumPostsAsync(ForumAllViewModel model)
        {
            string? tag = model?.TagName;
            string? query = model!.QueryString;

            IQueryable<ForumPost> filteredForums = dbContext.ForumPosts
                .Include(fp => fp.Tag)
                .Where(fp => fp.IsDeleted == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(tag))
            {
                filteredForums = filteredForums.Where(fp => fp.Tag.Name.ToLower() == tag.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(query))
            {
                string wildCard = $"%{query.ToLower()}%";
                filteredForums = filteredForums.Where(fp => EF.Functions.Like(fp.Title, wildCard));
            }


            IEnumerable<ForumDisplayViewModel> forums = await filteredForums.Select(fp => new ForumDisplayViewModel
            {
                Id = fp.Id.ToString(),
                Title = fp.Title,
                Tag = fp.Tag.Name,
                DatePosted = fp.DatePosted,
            }).ToArrayAsync();

            return forums;
        }

        public async Task<ForumReadViewModel> GetForumAsReadViewModelAsync(string id)
        {
            ForumReadViewModel model = await dbContext.ForumPosts
                .Include(fp => fp.Author)
                .Include(fp => fp.Tag)
                .AsNoTracking()
                .Where(fp => fp.Id.ToString().ToLower() == id)
                .Select(fp => new ForumReadViewModel
                {
                    Id = fp.Id.ToString(),
                    AuthorId = fp.AuthorId.ToString(),
                    AuthorName = fp.Author.UserName,
                    Content = fp.Content,
                    DateCreated = fp.DatePosted,
                    DateEdited = fp.DateEdited,
                    TagName = fp.Tag.Name,
                    Title = fp.Title
                }).FirstAsync();
            return model;
        }

        public async Task<ForumDetailsViewModel> GetForumAsDetailsModelAsync(string id)
        {
            ForumDetailsViewModel model = await dbContext.ForumPosts
                .Include(fp => fp.Author)
                .Include(fp => fp.Tag)
                .AsNoTracking()
                .Where(fp => fp.Id.ToString().ToLower() == id)
                .Select(fp => new ForumDetailsViewModel
                {
                    Id = fp.Id.ToString(),
                    Content = fp.Content,
                    Title = fp.Title,
                    DateCreated = fp.DatePosted,
                    DateEdited = fp.DateEdited,
                    TagName = fp.Tag.Name,
                }).FirstAsync();
            return model;
        }
    }
}
