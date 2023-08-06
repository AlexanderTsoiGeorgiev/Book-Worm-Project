namespace BookWorm.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BookWorm.Web.Models;
    using NToastNotify;

    using static BookWorm.Common.ToastMessages;

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
            //Testing Default Methods

            //Success
            toastNotification.AddSuccessToastMessage("Same for success message");
            // Success with default options (taking into account the overwritten defaults when initializing in Startup.cs)
            toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "poem"));

            //Info
            toastNotification.AddInfoToastMessage();

            //Warning
            toastNotification.AddWarningToastMessage();

            //Error
            toastNotification.AddErrorToastMessage();

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
            return RedirectToAction("All", "Poem");
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