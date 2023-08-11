namespace BookWorm.Web.Areas.Admin.Controllers
{
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.User;
    using Microsoft.AspNetCore.Mvc;
    using NToastNotify;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;
    using Microsoft.AspNetCore.Identity;
    using BookWorm.Data.Models;

    public class UserController : AdminBaseController
    {
        private readonly IUserService userService;
        private readonly IToastNotification toastNotification;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUserService userService, IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.toastNotification = toastNotification;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<UserAdminDisplayViewModel> model = await userService.GetAllUsersDisplayViewModelAsync();
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
        }

        [HttpGet]
        public IActionResult CreateModerator()
        {
            return View(new UserAdminModeratorForm());
        }

        [HttpPost]
        public async Task<IActionResult> CreateModerator(UserAdminModeratorForm model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                string username = model.UserName;
                ApplicationUser user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    toastNotification.AddErrorToastMessage("User with the specified username does not exists!");
                    return View(model);
                }
                IList<string> userRoles = await userManager.GetRolesAsync(user);
                bool isInRole = userRoles.Any(r => r == ModeratorRoleName);
                if (isInRole) 
                {
                    toastNotification.AddErrorToastMessage("User is already a moderator!");
                    return View(model);
                }

                await userService.CreateModeratorAsync(username);

                toastNotification.AddSuccessToastMessage("Succesffully added user to role!");
                return RedirectToAction("Index", "Home", new {Area = AdminAreaName});
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
        }
    }
}
