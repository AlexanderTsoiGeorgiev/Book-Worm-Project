namespace BookWorm.Data
{
    using BookWorm.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<BookPoem>(new BookPoemTableConfiguration());
            builder.ApplyConfiguration<Poem>(new PoemTableConfiguration());
            base.OnModelCreating(builder);
        }
    }
}