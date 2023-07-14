namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Review;

    public interface IReviewService
    {
        public Task CreateReviewAsync(ReviewFormViewModel model);
        public Task EditReviewAsync(string id, ReviewFormViewModel model);
        public Task SoftDeleteReviewAsync(string id);
    }
}
