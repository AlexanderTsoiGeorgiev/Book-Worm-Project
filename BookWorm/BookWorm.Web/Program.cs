namespace BookWorm.Web
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using NToastNotify;

    using BookWorm.Data;
    using BookWorm.Services;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.Infrastructure.ModelBinders;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    using static BookWorm.Common.GeneralApplicationConstants;
    using static BookWorm.Data.Common.AuthorIds;
    using static BookWorm.Data.Common.StaffIds;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<BookWormDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("IdentityConfiguration:SignIn:RequireConfirmedAccount");

                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("IdentityConfiguration:Password:RequireNonAlphanumerical");

                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("IdentityConfiguration:Password:RequireUppercase");

                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("IdentityConfiguration:Password:RequireLowercase");

                options.Password.RequiredLength = builder.Configuration.GetValue<int>("IdentityConfiguration:Password:RequiredLength");
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<BookWormDbContext>();

            builder.Services.AddControllersWithViews()
                .AddNToastNotifyToastr(new ToastrOptions
                {
                    ProgressBar = true,
                    CloseButton = true,
                    TimeOut = 5000
                })
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });


            builder.Services.AddScoped<IPoemService, PoemService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IForumPostService, ForumPostService>();
            builder.Services.AddScoped<IReplyService, ReplyService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.ConfigureApplicationCookie(configure => 
            {
                configure.AccessDeniedPath = "/Home/Error/401"; 
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                //app.UseExceptionHandler("/Home/Error/500");
                //app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                string[] ids = new string[] { 
                    EdgarAllanPoeId, 
                    WilliamShakespeareId, 
                    EmilyDickinsonId, 
                    AdminId, 
                    Moderator1Id, 
                    Moderator2Id 
                };
                app.AddFriendlyNameToSeededUsers(ids);
                app.SeedAdministator(AdminUserEmail);
                string[] emails = new string[] { Moderator1UserEmail, Moderator2UserEmail };
                app.SeedModerators(emails);
            }

            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "areas",
                       pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });

            app.Run();
        }
    }
}