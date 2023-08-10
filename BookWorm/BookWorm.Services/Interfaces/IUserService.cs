namespace BookWorm.Services.Interfaces
{
    using BookWorm.Web.ViewModels.User;

    public interface IUserService
    {
        //All
        public Task<IEnumerable<UserAdminDisplayViewModel>> GetAllUsersDisplayViewModelAsync();

        //Create
        public Task CreateModeratorAsync(string userName);
    }
}
