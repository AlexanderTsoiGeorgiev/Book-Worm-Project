namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Review;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class ReviewService : IReviewService
    {
        private readonly BookWormDbContext dbContext;

        public ReviewService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //TODO: Check if moddel is null and add try catch
        public async Task CreateReviewAsync(ReviewFormViewModel model)
        {
            if (model == null)
            {
                return;
            }

            Review entity = new Review
            {
                Content = model.Content,
                Rating = model.Rating,
            };

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        //TODO: Check if entity is null and add try catch
        public async Task EditReviewAsync(string id, ReviewFormViewModel model)
        {
            Review? entity = await dbContext.Reviews.FindAsync(Guid.Parse(id));

            if (entity == null)
            {
                return;
            }

            entity.Content = model.Content;
            entity.Rating = model.Rating;

            await dbContext.SaveChangesAsync();
        }
    }
}
