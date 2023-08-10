namespace BookWorm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.User;

    using static BookWorm.Common.GeneralApplicationConstants;
    using BookWorm.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly BookWormDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserService(BookWormDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task CreateModeratorAsync(string userName)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(userName);

            List<string> userRoles = await userManager.GetRolesAsync(user);
            bool inRole = await userRoles.AnyAsync();
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
    }
}
