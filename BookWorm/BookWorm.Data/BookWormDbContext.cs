namespace BookWorm.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class BookWormDbContext : IdentityDbContext
    {
        public BookWormDbContext(DbContextOptions<BookWormDbContext> options)
            : base(options)
        {
        }
    }
}