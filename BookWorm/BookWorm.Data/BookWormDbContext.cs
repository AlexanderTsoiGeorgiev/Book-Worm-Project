namespace BookWorm.Data
{
    using System.Reflection;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using BookWorm.Data.Models;
    using BookWorm.Data.Configuration;

    public class BookWormDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public BookWormDbContext(DbContextOptions<BookWormDbContext> options)
            : base(options)
        {
        }

        public DbSet<Poem> Poems { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<BookPoem> BookPoem { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<ForumPost> ForumPosts { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Reply> Replies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .ApplyConfiguration<ApplicationUser>(new ApplicationUserTableConfiguration())
                .ApplyConfiguration<Category>(new CategoryTableConfiguration())
                .ApplyConfiguration<BookPoem>(new BookPoemTableConfiguration())
                .ApplyConfiguration<Poem>(new PoemTableConfiguration())
                .ApplyConfiguration<Reply>(new ReplyTableConfiguration())
                .ApplyConfiguration<Tag>(new TagTableConfiguration());

            //base.OnModelCreating(builder);

            //Assembly configAssembly = Assembly.GetAssembly(typeof(ApplicationUserTableConfiguration)) ??
            //                          Assembly.GetExecutingAssembly();
            //builder.ApplyConfigurationsFromAssembly(configAssembly);


            base.OnModelCreating(builder);
        }
    }
}