namespace BookWorm.Services
{
    using BookWorm.Data;
    using BookWorm.Services.Interfaces;

    public class ReviewService : IReviewService
    {
        private readonly BookWormDbContext dbContext;

        public ReviewService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
