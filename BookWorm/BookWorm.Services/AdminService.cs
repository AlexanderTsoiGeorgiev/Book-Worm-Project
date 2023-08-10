namespace BookWorm.Services
{
    using BookWorm.Data;
    using BookWorm.Services.Interfaces;

    public class AdminService : IAdminService
    {
        private readonly BookWormDbContext dbContext;

        public AdminService(BookWormDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
    }
}
