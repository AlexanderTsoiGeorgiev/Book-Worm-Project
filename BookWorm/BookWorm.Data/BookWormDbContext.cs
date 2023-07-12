namespace BookWorm.Data
{
    using BookWorm.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class BookWormDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public BookWormDbContext(DbContextOptions<BookWormDbContext> options)
            : base(options)
        {
        }
    }
}