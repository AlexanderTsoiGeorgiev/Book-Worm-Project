namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Review;

    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDisplayViewModel>> GetAllUserReviews(string userId);
        public Task CreateReviewAsync(ReviewFormViewModel model);
        public Task EditReviewAsync(string id, ReviewFormViewModel model);
        public Task SoftDeleteReviewAsync(string id);
    }
}
