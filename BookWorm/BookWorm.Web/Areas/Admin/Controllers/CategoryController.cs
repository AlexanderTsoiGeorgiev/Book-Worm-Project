namespace BookWorm.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Category;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class CategoryController : AdminBaseController
    {
        private readonly IAdminService adminService;
        private readonly ICategoryService categoryService;
        private readonly IToastNotification toastNotification;

        public CategoryController(
            IAdminService adminService,
            ICategoryService categoryService,
            IToastNotification toastNotification
            )
        {
            this.adminService = adminService;
            this.categoryService = categoryService;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<CategoryDisplayViewModel> model =
                    await categoryService.GetAllCategoriesAsDisplayModelAsync();

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }

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
                bool exists = await categoryService.CategoryExistsByNameAsync(model);
                if (exists)
                {
                    toastNotification.AddWarningToastMessage("Category with such name already exists!");
                    return View(model);
                }

                await categoryService.AddCategoryAsync(model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "category"));
                return RedirectToAction("Index", "Category", new { Area = AdminAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool existsById = await categoryService.CategoryExistsByIdAsync(id);
                if (!existsById) 
                {
                    toastNotification.AddWarningToastMessage("Category with such id does not exist!");
                    return RedirectToAction("Index", "Category", new {Area = AdminAreaName});
                }

                await categoryService.SoftDeleteCategoryAsync(id);
                toastNotification.AddSuccessToastMessage("Successfully deleted category!");
                return RedirectToAction("Index", "Category", new { Area = AdminAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                bool existsById = await categoryService.CategoryExistsByIdAsync(id);
                if (!existsById)
                {
                    toastNotification.AddWarningToastMessage("Category with such id does not exist!");
                    return RedirectToAction("Index", "Category", new { Area = AdminAreaName });
                }

                await categoryService.RestoreCategoryAsync(id);
                toastNotification.AddSuccessToastMessage("Successfully restored category!");
                return RedirectToAction("Index", "Category", new { Area = AdminAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
        }
    }
}
