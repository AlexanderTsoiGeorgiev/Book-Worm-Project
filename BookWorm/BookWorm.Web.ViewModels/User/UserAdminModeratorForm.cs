namespace BookWorm.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserAdminModeratorForm
    {
        [Required]
        public string UserName { get; set; } = null!;
    }
}
