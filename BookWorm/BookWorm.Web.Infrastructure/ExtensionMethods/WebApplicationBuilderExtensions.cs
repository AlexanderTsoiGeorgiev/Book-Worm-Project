namespace BookWorm.Web.Infrastructure.ExtensionMethods
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using BookWorm.Data;
    using BookWorm.Data.Models;

    using static BookWorm.Common.ClaimNamesConstants;
    using static BookWorm.Common.GeneralApplicationConstants;

    public static class WebApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddFriendlyNameToSeededUsers(this IApplicationBuilder app, string[] ids)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            BookWormDbContext dbContext = serviceProvider.GetRequiredService<BookWormDbContext>();
            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationUser user;
            
            Task.Run(async () =>
            {
                IEnumerable<Claim> claims;
                foreach (string id in ids)
                {
                    user = await userManager.FindByIdAsync(id);
                    Claim friendlyName = new Claim(FriendlyNameClaimName, user.FirstName);
                    claims = await userManager.GetClaimsAsync(user);
                    if (claims.Any(c => c.Type == FriendlyNameClaimName))
                    {
                        continue;
                    }
                    await userManager.AddClaimAsync(user, friendlyName);
                    
                }
            })
            .GetAwaiter()
            .GetResult();


            return app;
        }

        public static IApplicationBuilder SeedAdministator(this IApplicationBuilder app, string email)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole<Guid>> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                IdentityRole<Guid> role =
                    new IdentityRole<Guid>(AdminRoleName);

                await roleManager.CreateAsync(role);

                ApplicationUser adminUser =
                    await userManager.FindByEmailAsync(email);

                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
            })
           .GetAwaiter()
           .GetResult();

            return app;
        }

        public static IApplicationBuilder SeedModerators(this IApplicationBuilder app, string[] emails)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole<Guid>> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(ModeratorRoleName))
                {
                    return;
                }

                IdentityRole<Guid> role =
                    new IdentityRole<Guid>(ModeratorRoleName);

                await roleManager.CreateAsync(role);

                ApplicationUser moderatorUser;
                foreach (string email in emails)
                {
                    moderatorUser = await userManager.FindByEmailAsync(email);
                    await userManager.AddToRoleAsync(moderatorUser, ModeratorRoleName);
                }

            })
           .GetAwaiter()
           .GetResult();

            return app;
        }

       
    }
}