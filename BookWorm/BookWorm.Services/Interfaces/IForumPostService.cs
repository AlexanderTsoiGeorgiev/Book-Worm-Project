namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Forum;

    public interface IForumPostService
    {
        //Add
        public Task AddForumPostAsync(string authorId, ForumFormViewModel model);

        //Edit
        public Task EditForumPostAsync(string id, ForumFormViewModel model);
        public Task<ForumFormViewModel> FindForumPostAsForumFormModelAsync(string id);

        //Delete
        public Task SoftDeletePostAsync(string id);


        //Validation
        public Task<bool> ForumPostExistsAsync(string id);
        public Task<bool> IsPostDeletedAsync(string id);
        public Task<bool> IsUserOwnerAsync(string authorId, string postId);
    }
}
