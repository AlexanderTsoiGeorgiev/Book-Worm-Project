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
        private readonly ICategoryService categoryService;
        private readonly IToastNotification toastNotification;

        public Category(
            IAdminService adminService, 
            ICategoryService categoryService, 
            IToastNotification toastNotification
            )
        {
            this.adminService = adminService;
            this.categoryService = categoryService;
            this.toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok();
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
                bool exists = await categoryService.CategoryExistsAsync(model);
                toastNotification.AddWarningToastMessage("Category with such name already exists!");
                if (exists) return View(model);

                await categoryService.AddCategoryAsync(model);
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
