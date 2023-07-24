namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Review;

    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDisplayViewModel>> GetAllUserReviewsAsync(string userId);
        public Task<IEnumerable<ReviewDisplayViewModel>> GetAllPoemReviewsAsync(string poemId);


        //
        public Task CreatePoemReviewAsync(string authorId, ReviewFormViewModel model);
        public Task CreateBookReviewAsync(string authorId, ReviewFormViewModel model);

        //Edit
        public Task<ReviewFormViewModel> FindReviewByIdAsync(string id);
        public Task EditReviewAsync(string id, ReviewFormViewModel model);

        public Task SoftDeleteReviewAsync(string id);

        //Validation
        public Task<bool> ExistsByIdAsync(string id);
        public Task<bool> IsReviewDeletedAsync(string id);
        public Task<bool> IsUserReviewOwnerAsync(string authorId, string id)
    }
}
