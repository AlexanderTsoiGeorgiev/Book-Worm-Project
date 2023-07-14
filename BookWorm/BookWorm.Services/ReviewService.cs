namespace BookWorm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Review;

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

        //TODO: Add exceptions
        public async Task<IEnumerable<ReviewDisplayViewModel>> GetAllUserReviews(string userId)
        {
            var userReviews = await dbContext.Reviews
                .Include(r => r.Poem)
                .Include(r => r.Book)
                .Where(r => r.AuthorId.ToString() == userId &&
                                      r.IsDeleted == false)
                .Select(r => new ReviewDisplayViewModel
                {
                    Content = r.Content,
                    Rating = r.Rating,
                    DatePosted = r.DatePosted,
                    DateEdited = r.DateEdited,
                    Upvotes = r.Upvotes,
                    Downvotes = r.Downvotes,
                    PoemName = r.Poem.Title ?? string.Empty, //??
                    BookName = r.Book.Title ?? string.Empty, //??
                })
                .ToArrayAsync();

            if (userReviews.Any())
            {
                throw new Exception();
            }

            return userReviews;
        }

        //TODO: Check if entity is null and add try catch
        public async Task SoftDeleteReviewAsync(string id)
        {
            Review? entity = await dbContext.Reviews.FindAsync(Guid.Parse(id));

            if (entity == null)
            {
                return;
            }

            entity.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }
    }
}
