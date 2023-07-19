﻿namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.Review;

    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDisplayViewModel>> GetAllUserReviews(string userId);

        //
        public Task CreatePoemReviewAsync(string authorId, ReviewFormViewModel model);
        public Task CreateBookReviewAsync(string authorId, ReviewFormViewModel model);

        public Task<ReviewFormViewModel> FindReviewById(string id);
        public Task EditReviewAsync(string id, ReviewFormViewModel model);

        public Task SoftDeleteReviewAsync(string id);
    }
}
