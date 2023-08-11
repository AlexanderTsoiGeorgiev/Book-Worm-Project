namespace BookWorm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.User;

    using static BookWorm.Common.GeneralApplicationConstants;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Data.Common;

    public class UserService : IUserService
    {
        private readonly BookWormDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(BookWormDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task CreateModeratorAsync(string userName)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(userName);
            await userManager.AddToRoleAsync(user, ModeratorRoleName);
        }

        public async Task<IEnumerable<UserAdminDisplayViewModel>> GetAllUsersDisplayViewModelAsync()
        {
            UserAdminDisplayViewModel[] users = await dbContext
                .Users
                .AsNoTracking()
                .Where(u => u.UserName != AdminUserEmail)
                .Select(u => new UserAdminDisplayViewModel
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName
                }).ToArrayAsync();

            return users;
        }


        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            return user;
        }

        //Load Poems
        public async Task<IEnumerable<PoemDisplayViewModel>> GetUserPoemsAsDisplayModelAsync(string id)
        {
            PoemDisplayViewModel[] poems = await dbContext.Poems
                .AsNoTracking()
                .Where(p => p.AuthorId.ToString() == id && p.IsDeleted == false && p.IsPrivate == false)
                .Select(p => new PoemDisplayViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    DateCreated = p.DateCreated,
                }).ToArrayAsync();

            return poems;
        }

        //Load Books
        public async Task<IEnumerable<BookDisplayViewModel>> GetUserBooksAsDisplayModelAsync(string id)
        {
            string userName = dbContext.Users.Find(Guid.Parse(id))!.UserName;
            BookDisplayViewModel[] books = await dbContext.Books
                .AsNoTracking()
                .Where(b => b.AuthorId.ToString() == id && b.IsDeleted == false)
                .Select(b => new BookDisplayViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    AuthorName = userName
                }).ToArrayAsync();

            return books;
        }
    }
}
