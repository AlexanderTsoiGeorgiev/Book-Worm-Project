namespace BookWorm.Web.Areas.Admin.Controllers
{
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Tag;
    using Microsoft.AspNetCore.Mvc;
    using NToastNotify;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;
    using BookWorm.Services;

    public class TagController : AdminBaseController
    {
        private readonly ITagService tagService;
        private readonly IToastNotification toastNotification;

        public TagController(ITagService tagService, IToastNotification toastNotification)
        {
            this.tagService = tagService;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<TagDisplayViewModel> model = await tagService.AllTagsAsDisplayModelAsync();
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
            return View(new TagFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(TagFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }
            try
            {
                string name = model.Name;
                bool exists = await tagService.TagExistsByNameAsync(name);
                if (exists)
                {
                    toastNotification.AddErrorToastMessage("Tag with such name already exists!");
                    return View(model);
                }

                await tagService.AddTagAsync(model);
                toastNotification.AddSuccessToastMessage("Successfully added tag!");
                return RedirectToAction("Index", "Tag", new { Area = AdminAreaName });
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
                bool existsById = await tagService.TagExistsByIdAsync(id);
                if (!existsById)
                {
                    toastNotification.AddWarningToastMessage("Tag with such id does not exist!");
                    return RedirectToAction("Index", "Tag", new { Area = AdminAreaName });
                }

                await tagService.SoftDeleteAsync(id);
                toastNotification.AddSuccessToastMessage("Successfully deleted tag!");
                return RedirectToAction("Index", "Tag", new { Area = AdminAreaName });
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
                bool existsById = await tagService.TagExistsByIdAsync(id);
                if (!existsById)
                {
                    toastNotification.AddWarningToastMessage("Tag with such id does not exist!");
                    return RedirectToAction("Index", "Tag", new { Area = AdminAreaName });
                }

                await tagService.RestoreAsync(id);
                toastNotification.AddSuccessToastMessage("Successfully restored tag!");
                return RedirectToAction("Index", "Tag", new { Area = AdminAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
        }
    }
}
