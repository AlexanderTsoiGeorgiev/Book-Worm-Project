namespace BookWorm.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Category;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class Category : AdminBaseController
    {
        private readonly IAdminService adminService;
        private readonly IToastNotification toastNotification;

        public Category(IAdminService adminService, IToastNotification toastNotification)
        {
            this.adminService = adminService;
            this.toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new CategoryFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryFormViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                await adminService.AddCategoryAsync(model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "category"));
                return RedirectToAction(nameof(Add), "Category", new { Area = AdminAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
