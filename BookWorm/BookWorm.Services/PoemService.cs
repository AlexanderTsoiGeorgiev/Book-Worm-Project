namespace BookWorm.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Web.ViewModels.Poem.Enums;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Category;
    using BookWorm.Services.Models.Poem;


    //TODO: Add exceptions and add summaries
    public class PoemService : IPoemService
    {
        private readonly BookWormDbContext dbContext;

        public PoemService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //All
        public async Task<IEnumerable<PoemDisplayViewModel>> GetAllPoemsAsync()
        {
            PoemDisplayViewModel[] allPoems = await dbContext.Poems.AsNoTracking().Select(p => new PoemDisplayViewModel
            {
                Title = p.Title,
                Description = p.Description,
                DateCreated = p.DateCreated
            }).ToArrayAsync();

            return allPoems;
        }
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
                    filteredPoems = filteredPoems.OrderBy(p => p.DateCreated);
                    break;
                case PoemSort.Oldest:
                    filteredPoems = filteredPoems.OrderByDescending(p => p.DateCreated);
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
        //TODO: Check comment
        public async Task<PoemReadViewModel> FindPoemReadModelByIdAsync(string id)
        {
            PoemReadViewModel? entity = await dbContext.Poems
                .Include(p => p.Reviews)
                .Include(p => p.Author)
                .AsNoTracking()
                .Where(p => p.Id.ToString() == id)
                .Select(p => new PoemReadViewModel
                {
                    Title = p.Title,
                    ReadableContent = p.Content
                                              .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                              .ToArray(),
                    DateCreated = p.DateCreated,
                    DateEdited = p.DateEdited,
                    AuthorName = p.Author.UserName,
                    AuthorId = p.AuthorId,
                }).FirstOrDefaultAsync();


            //Instead of throwing exceptions return bad request in controller
            if (entity == null)
            {
                throw new Exception();
            }

            return entity;
        }

        //Add
        //Check TODOs
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
        //Check TODOs
        public async Task EditPoemAsync(string id, PoemFormViemModel model)
        {
            var entity = await dbContext.Poems.FindAsync(Guid.Parse(id));
            if (entity == null)
            {
                return; //Add exception
            }
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.Description = model.Description;
            entity.IsPrivate = model.IsPrivate;
            entity.DateEdited = DateTime.Now;

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
                .FirstOrDefaultAsync();

            if (poem == null)
            {
                throw new Exception();
            }

            return poem;
        }
        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await dbContext.Poems.FirstOrDefaultAsync(p => p.Id.ToString() == id) != null;
        }
        public async Task<bool> IsUserPoemOwnerAsync(string userId, string poemId)
        {
            Poem? entity = await dbContext.Poems.FindAsync(Guid.Parse(poemId));

            return entity!.AuthorId == Guid.Parse(userId);
        }

        //Mine
        //Check TODOs
        public async Task<IEnumerable<PoemDisplayViewModel>> GetAllUserPoemsAsync(string id)
        {
            var allPoems = await dbContext.Poems
                .AsNoTracking()
                .Where(p => p.IsDeleted == false && p.AuthorId.ToString() == id)
                .Select(p => new PoemDisplayViewModel
                {
                    Title = p.Title,
                    DateCreated = p.DateCreated,
                    Description = p.Description
                })
                .ToArrayAsync();
            return allPoems;
        }

        //Delete
        //Check TODOs
        public async Task SoftDeletePoemAsync(string id)
        {
            var entity = await dbContext.Poems.FindAsync(id);
            if (entity == null)
            {
                return; //Add exception
            }
            entity.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        //NOTE: This many require its own service
        public async Task<IEnumerable<CategoryDisplayViewModel>> GetAllCategoriesAsync()
        {
            CategoryDisplayViewModel[]? categories = await dbContext.Categories
                .Select(c => new CategoryDisplayViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            if (!categories.Any())
            {
                throw new Exception();
            }

            return categories;
        }
        public async Task<IEnumerable<string>> GetAllCategoryNamesAsync()
        {
            string[] categoryNames = await dbContext.Categories
                .AsNoTracking()
                .Select(c => c.Name)
                .ToArrayAsync();

            if (!categoryNames.Any())
            {
                throw new Exception();
            }

            return categoryNames;
        }

        
    }
}
