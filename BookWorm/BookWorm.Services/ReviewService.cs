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

        //Add
        public async Task CreatePoemReviewAsync(string authorId, ReviewFormViewModel model)
        {
            Review entity = new Review
            {
                Title = model.Title,
                Content = model.Content,
                Rating = model.Rating,
                PoemId = model.PoemId,
                DatePosted = DateTime.Now,
                AuthorId = Guid.Parse(authorId)
            };

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }
        public async Task CreateBookReviewAsync(string authorId, ReviewFormViewModel model)
        {
            Review entity = new Review
            {
                Title = model.Title,
                Content = model.Content,
                Rating = model.Rating,
                BookId = model.BookId,
                DatePosted = DateTime.Now,
                AuthorId = Guid.Parse(authorId)
            };

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        //PoemRead
        public async Task<IEnumerable<ReviewDisplayViewModel>?> GetAllPoemReviewsAsync(string poemId)
        {
            ReviewDisplayViewModel[]? reviews = await dbContext.Reviews
                .Include(r => r.Poem)
                .Include(r => r.Author)
                .Where(r => r.PoemId != null &&
                            r.PoemId.ToString() == poemId)
                .Select(r => new ReviewDisplayViewModel
                {
                    Id = r.Id,
                    PoemId = r.PoemId,
                    Title = r.Title,
                    Content = r.Content,
                    Upvotes = r.Upvotes,
                    Downvotes = r.Downvotes,
                    AuthorId = r.AuthorId,
                    AuthorName = r.Author.UserName,
                    Rating = r.Rating,
                    DatePosted = r.DatePosted,
                    DateEdited = r.DateEdited
                }).ToArrayAsync();


            return reviews;
        }

        //Edit
        public async Task EditReviewAsync(string id, ReviewFormViewModel model)
        {
            Review? entity = await dbContext.Reviews.FindAsync(Guid.Parse(id));

            entity!.Content = model.Content;
            entity!.Rating = model.Rating;
            entity!.Title = model.Title;
            entity!.DateEdited = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }

        //Delete
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

        //Like
        public async Task LikeReviewAsync(string id)
        {
            Review? review = await dbContext.Reviews.FindAsync(Guid.Parse(id));
            review!.Upvotes++;

            await dbContext.SaveChangesAsync();
        }

        //Dislike

        //Details
        public async Task<ReviewDetailsViewModel> GetReviewAsDetailsViewModelAsync(string id)
        {
            ReviewDetailsViewModel model = await dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.Id.ToString() == id)
                .Select(r => new ReviewDetailsViewModel
                {
                    Id = r.Id.ToString(),
                    PoemId = r.PoemId.ToString(),
                    BookId = r.BookId,
                    Title = r.Title,
                    Content = r.Content,
                    DateEdited = r.DateEdited,
                    Downvotes = r.Downvotes,
                    Upvotes = r.Upvotes,
                    Rating = r.Rating,
                    DateCreated = r.DatePosted
                }).FirstAsync();

            return model;
        }

        //Mine
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
                    Id = r.Id,
                    PoemId = r.PoemId,
                    BookId = r.BookId,
                    Title = r.Title,
                    Content = r.Content,
                    Upvotes = r.Upvotes,
                    Downvotes = r.Downvotes,
                    AuthorId = r.AuthorId,
                    AuthorName = r.Author.UserName,
                    Rating = r.Rating,
                    DatePosted = r.DatePosted,
                    DateEdited = r.DateEdited
                })
                .ToArrayAsync();

            return userReviews;
        }

        //Validation
        public async Task<bool> ExistsByIdAsync(string id)
        {
            Review? review = await dbContext.Reviews.FindAsync(Guid.Parse(id));
            return review != null;
        }
        public async Task<bool> IsReviewDeletedAsync(string id)
        {
            Review? review = await dbContext.Reviews.FindAsync(Guid.Parse(id));
            return review!.IsDeleted;
        }
        public async Task<bool> IsUserReviewOwnerAsync(string authorId, string id)
        {
            Review? review = await dbContext.Reviews.FindAsync(Guid.Parse(id));
            return review!.AuthorId.ToString() == authorId;
        }

        //Utitlity      
        public async Task<ReviewFormViewModel> FindReviewByIdAsync(string id)
        {
            ReviewFormViewModel review = await dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.Id.ToString() == id)
                .Select(r => new ReviewFormViewModel
                {
                    Title = r.Title,
                    Content = r.Content,
                    Rating = r.Rating,
                    PoemId = r.PoemId,
                    BookId = r.BookId
                })
                .FirstAsync();

            return review;
        }
        public async Task<string?> RetriveReviewPoemIdAsync(string id)
        {
            Review? review = await dbContext.Reviews.FindAsync(Guid.Parse(id));

            return review!.PoemId.ToString();
        }
        public async Task<int?> RetriveReviewBookIdAsync(string id)
        {
            Review? review = await dbContext.Reviews.FindAsync(Guid.Parse(id));

            return review!.BookId;
        }
    }
}
