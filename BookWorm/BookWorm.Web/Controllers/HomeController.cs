namespace BookWorm.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BookWorm.Web.Models;
    using NToastNotify;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class HomeController : BaseController
    {
        private readonly IToastNotification toastNotification;

        public HomeController(IToastNotification toastNotification)
        {
            this.toastNotification = toastNotification;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return View("Error400");
                case 404:
                    return View("Error404");
                default:
                    return View();
            }
        }
    }
}