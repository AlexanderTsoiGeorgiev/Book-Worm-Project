namespace BookWorm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Reply;
    using BookWorm.Data.Models;

    public class ReplyService : IReplyService
    {
        private readonly BookWormDbContext dbContext;

        public ReplyService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddReplyAsync(ReplyFormViewModel model, string authorId)
        {
            Reply reply = new Reply()
            {
                Content = model.Content,
                AuthorId = Guid.Parse(authorId),
                ForumPostId = Guid.Parse(model.PostId!)
            };

            await dbContext.AddAsync(reply);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReplyDisplayViewModel>> GetAllPostReplyAsync(string id)
        {
            IEnumerable<ReplyDisplayViewModel> model = await dbContext.Replies
                .Include(r => r.Author)
                .AsNoTracking()
                .Where(r => r.ForumPostId.ToString().ToLower() == id.ToLower())
                .Select(r => new ReplyDisplayViewModel
                {
                    AuthorId = r.AuthorId.ToString(),
                    Content = r.Content,
                    ForumPostId = id,
                    ReplyId = r.Id.ToString(),
                    AuthorName = r.Author.UserName
                }).ToArrayAsync();

            return model;
        }
    }
}
