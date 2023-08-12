namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Reply;

    public interface IReplyService
    {
        public Task AddReplyAsync(ReplyFormViewModel model, string authorId);

        public Task<IEnumerable<ReplyDisplayViewModel>> GetAllPostReplyAsync(string id);
    }
}
