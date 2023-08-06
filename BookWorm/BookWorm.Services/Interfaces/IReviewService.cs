namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Review;

    public interface IReviewService
    {
        //Poem Read
        public Task<IEnumerable<ReviewDisplayViewModel>?> GetAllPoemReviewsAsync(string poemId);

        //Add
        public Task CreatePoemReviewAsync(string authorId, ReviewFormViewModel model);
        public Task CreateBookReviewAsync(string authorId, ReviewFormViewModel model);

        //Edit
        public Task EditReviewAsync(string id, ReviewFormViewModel model);

        //Delete
        public Task SoftDeleteReviewAsync(string id);

        //Like  
        public Task LikeReviewAsync(string id);

        //Details
        public Task<ReviewDetailsViewModel> GetReviewAsDetailsViewModelAsync(string id);

        //Mine
        public Task<IEnumerable<ReviewDisplayViewModel>> GetAllUserReviewsAsync(string userId);

        //Validation
        public Task<bool> ExistsByIdAsync(string id);
        public Task<bool> IsReviewDeletedAsync(string id);
        public Task<bool> IsUserReviewOwnerAsync(string authorId, string id);

        //Utility
        public Task<ReviewFormViewModel> FindReviewByIdAsync(string id);
        public Task<string?> RetriveReviewPoemIdAsync(string id);
        public Task<int?> RetriveReviewBookIdAsync(string id);
    }
}
