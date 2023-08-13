namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Web.ViewModels.Poem.Enums;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Services.Models.Poem;
    using BookWorm.Web.ViewModels.Category;


    public class PoemService : IPoemService
    {
        private readonly BookWormDbContext dbContext;

        public PoemService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //All
        public async Task<PoemAllFilteredServiceModel> GetAllPoemsFilteredAsync(PoemQueryViewModel query)
        {
            IQueryable<Poem> filteredPoems = dbContext.Poems
                .Include(p => p.Category)
                .Include(p => p.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CategoryName))
            {
                filteredPoems = filteredPoems
                    .Where(p => p.Category.Name == query.CategoryName);
            }

            if (!string.IsNullOrEmpty(query.QueryString))
            {
                string wildCard = $"%{query.QueryString.ToLower()}%";

                filteredPoems = filteredPoems.
                    Where(p => EF.Functions.Like(p.Title, wildCard) ||
                               EF.Functions.Like(p.Description, wildCard) ||
                               EF.Functions.Like(p.Author.UserName, wildCard));
            }

            switch (query.OrderBy)
            {
                case PoemSort.AlphabeticallyAscending:
                    filteredPoems = filteredPoems.OrderBy(p => p.Title);
                    break;
                case PoemSort.AlphabeticallyDescending:
                    filteredPoems = filteredPoems.OrderByDescending(p => p.Title);
                    break;
                case PoemSort.Newest:
                    filteredPoems = filteredPoems.OrderByDescending(p => p.DateCreated);
                    break;
                case PoemSort.Oldest:
                    filteredPoems = filteredPoems.OrderBy(p => p.DateCreated);
                    break;
                default:
                    filteredPoems = filteredPoems.OrderBy(p => p.DateCreated);
                    break;
            }

            IEnumerable<PoemDisplayViewModel> poemsOnCurrentPage = await filteredPoems
               .Where(p => p.IsDeleted == false && p.IsPrivate == false)
               .Skip((query.CurrentPage - 1) * query.PoemsPerPage)
               .Take(query.PoemsPerPage)
               .Select(p => new PoemDisplayViewModel
               {
                   Id = p.Id,
                   Title = p.Title,
                   Description = p.Description,
                   DateCreated = p.DateCreated
               })
               .ToArrayAsync();

            int totalPoems = filteredPoems.Count();

            return new PoemAllFilteredServiceModel()
            {
                Poems = poemsOnCurrentPage,
                AllPoemsCount = totalPoems
            };

        }

        //Read
        public async Task<PoemReadViewModel> FindPoemReadModelByIdAsync(string id)
        {
            PoemReadViewModel? entity = await dbContext.Poems
                .Include(p => p.Reviews)
                .Include(p => p.Author)
                .AsNoTracking()
                .Where(p => p.Id.ToString() == id)
                .Select(p => new PoemReadViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    ReadableContent = SplitContentToReadableFormat(p.Content),
                    DateCreated = p.DateCreated,
                    DateEdited = p.DateEdited,
                    AuthorName = p.Author.UserName,
                    AuthorId = p.AuthorId,
                }).FirstOrDefaultAsync();

            return entity!;
        }

        //Add
        public async Task CreatePoemAsync(string authorId, PoemFormViemModel model)
        {
            var entity = new Poem
            {
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                IsPrivate = model.IsPrivate,
                DateCreated = DateTime.Now,
                AuthorId = Guid.Parse(authorId),
                CategoryId = model.CategoryId
            };

            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        //Edit
        public async Task EditPoemAsync(string id, PoemFormViemModel model)
        {
            Poem? entity = await dbContext.Poems.FindAsync(Guid.Parse(id));
            
            entity!.Title = model.Title;
            entity!.Content = model.Content;
            entity!.Description = model.Description;
            entity!.IsPrivate = model.IsPrivate;
            entity!.DateEdited = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }
        public async Task<PoemFormViemModel> FindPoemByIdAsync(string id)
        {
            PoemFormViemModel? poem = await dbContext.Poems
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.Id.ToString() == id)
                .Select(p => new PoemFormViemModel
                {
                    Title = p.Title,
                    Description = p.Description,
                    Content = p.Content,
                    IsPrivate = p.IsPrivate,
                    CategoryId = p.CategoryId
                })
                .FirstAsync();

            return poem;
        }
        

        //Mine
        public async Task<IEnumerable<PoemDisplayViewModel>?> GetAllUserPoemsAsync(string id)
        {
            PoemDisplayViewModel[]? allPoems = await dbContext.Poems
                .AsNoTracking()
                .Where(p => p.IsDeleted == false && p.AuthorId.ToString() == id)
                .Select(p => new PoemDisplayViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    DateCreated = p.DateCreated,
                    Description = p.Description
                })
                .ToArrayAsync();
            return allPoems;
        }

        //Delete
        public async Task SoftDeletePoemAsync(string id)
        {
            Poem? entity = await dbContext.Poems.FindAsync(Guid.Parse(id));
            entity!.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }
        

        //NOTE: This many require its own service
        public async Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsync()
        {
            CategoryDisplayViewModel[]? categories = await dbContext.Categories
                .Where(c => c.IsDeleted == false)
                .Select(c => new CategoryDisplayViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return categories;
        }
        public async Task<IEnumerable<string>> GetAllCategoryNamesAsync()
        {
            string[] categoryNames = await dbContext.Categories
                .AsNoTracking()
                .Select(c => c.Name)
                .ToArrayAsync();

            return categoryNames;
        }


        //Details
        public async Task<PoemDetailsVisualizeViewModel?> GetPoemAsDetailsViewModelByIdAsync(string id)
        {
            PoemDetailsVisualizeViewModel? model = await dbContext.Poems
                .AsNoTracking()
                .Where(p => p.Id.ToString() == id)
                .Select(p => new PoemDetailsVisualizeViewModel
                {
                    Id = p.Id.ToString(),
                    ReadableContent = SplitContentToReadableFormat(p.Content),
                    Title = p.Title,
                    IsPrivate = p.IsPrivate,
                    DateCreated = p.DateCreated,
                    DateEdited = p.DateEdited
                }).FirstOrDefaultAsync();

            return model;
        }

        //Validation
        public async Task<bool> ExistsByIdAsync(string id)
        {
            Poem? poem = await dbContext.Poems.FindAsync(Guid.Parse(id));
            return poem != null;
        }
        public async Task<bool> IsPoemDeletedAsync(string id)
        {
            Poem? poem = await dbContext.Poems.FindAsync(Guid.Parse(id));
            return poem!.IsDeleted;
        }
        public async Task<bool> IsPoemPrivateAsync(string id)
        {
            Poem? poem = await dbContext.Poems.FindAsync(Guid.Parse(id));
            return poem!.IsPrivate;
        }
        public async Task<bool> IsUserPoemOwnerAsync(string userId, string poemId)
        {
            Poem? entity = await dbContext.Poems.FindAsync(Guid.Parse(poemId));

            return entity!.AuthorId == Guid.Parse(userId);
        }

        
        //Used by BookController
        public async Task<IEnumerable<PoemBookSelectViewModel>> GetUserPoemsAsPoemBookSelectViewModelAsync(string userId)
        {
            PoemBookSelectViewModel[] poems = await dbContext.Poems.AsNoTracking().Where(p => p.AuthorId.ToString() == userId).Select(p => new PoemBookSelectViewModel
            {
                Id = p.Id,
                Title = p.Title
            }).ToArrayAsync();

            return poems;
        }
        public async Task<bool> UserHasPoemsAsync(string id)
        {
            bool hasPoems = await dbContext.Poems.AnyAsync(p => p.AuthorId.ToString() == id);
            return hasPoems;
        }

        //Utility
        private static string[] SplitContentToReadableFormat(string content)
        {
            return content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
    }
}
