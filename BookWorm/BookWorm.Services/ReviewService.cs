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
        public async Task CreatePoemReviewAsync(string authorId, ReviewFormViewModel model)
        {
            if (model == null)
            {
                return;
            }

            Review entity = new Review
            {
                Content = model.Content,
                Rating = model.Rating,
                PoemId = model.PoemId
            };

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
        public async Task CreateBookReviewAsync(string authorId, ReviewFormViewModel model)
        {
            if (model == null)
            {
                return;
            }

            Review entity = new Review
            {
                Content = model.Content,
                Rating = model.Rating,
                BookId = model.BookId
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

        public async Task<ReviewFormViewModel> FindReviewById(string id)
        {
            ReviewFormViewModel? review = await dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.Id.ToString() == id)
                .Select(r => new ReviewFormViewModel
                {
                    Content = r.Content,
                    Rating = r.Rating,
                    PoemId = r.PoemId,
                    BookId = r.BookId
                })
                .FirstOrDefaultAsync();
            if (review == null)
            {
                throw new Exception();
            }

            return review;
        }

        //TODO: Add exceptions
        public async Task<IEnumerable<ReviewDisplayViewModel>> GetAllUserReviewsAsync(string userId)
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

        public async Task<IEnumerable<ReviewDisplayViewModel>> GetAllPoemReviewsAsync(string poemId)
        {
            ReviewDisplayViewModel[]? reviews = await dbContext.Reviews
                .Include(r => r.Poem)
                .Where(r => r.PoemId != null &&
                            r.PoemId.ToString() == poemId)
                .Select(r => new ReviewDisplayViewModel
                {
                    Content = r.Content,
                    Upvotes = r.Upvotes,
                    Downvotes = r.Downvotes,
                    Rating = r.Rating,
                    DatePosted = r.DatePosted,
                    DateEdited = r.DateEdited,
                    PoemName = r.Poem!.Title
                }).ToArrayAsync();


            return reviews;
        }
    }
}
