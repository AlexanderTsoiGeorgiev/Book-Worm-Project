namespace BookWorm.Web
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Services;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.Infrastructure.ModelBinders;
    using BookWorm.Web.Infrastructure.ExtensionMethods;
    using Microsoft.AspNetCore.Identity;

    using static BookWorm.Common.GeneralApplicationConstants;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

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

            builder.Services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });


            builder.Services.AddScoped<IPoemService, PoemService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
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
                app.SeedAdministator(AdminUserEmail);
                string[] emails = new string[] { Moderator1UserEmail, Moderator2UserEmail };
                app.SeedModerators(emails);
            }

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}