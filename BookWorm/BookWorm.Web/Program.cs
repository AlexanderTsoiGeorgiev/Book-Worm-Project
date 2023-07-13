namespace BookWorm.Web
{
    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Services;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;

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
                .AddEntityFrameworkStores<BookWormDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IPoemService, PoemService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}