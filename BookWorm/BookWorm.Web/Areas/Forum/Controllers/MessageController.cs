namespace BookWorm.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;

    using BookWorm.Web.ViewModels.Tag;
    using BookWorm.Services.Interfaces;

    using static BookWorm.Common.GeneralApplicationConstants;
    using static BookWorm.Common.ToastMessages;
    using BookWorm.Web.ViewModels.Forum;

    public class MessageController : ForumBaseController
    {
        private readonly ITagService tagService;
        private readonly IToastNotification toastNotification;

        public MessageController(ITagService tagService, IToastNotification toastNotification)
        {
            this.tagService = tagService;
            this.toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                IEnumerable<TagDisplayViewModel> tags = await tagService.AllTagsAsDisplayModelAsync();
                ForumFormViewModel model = new ForumFormViewModel()
                {
                    Tags = tags
                };

                return View(model);

            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
            }
        }
        [HttpPost]
        public IActionResult Add(int dummy)
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
