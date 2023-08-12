namespace BookWorm.Web.Areas.Forum.Controllers
{
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.Infrastructure.ExtensionMethods;
    using BookWorm.Web.ViewModels.Reply;
    using Microsoft.AspNetCore.Mvc;
    using NToastNotify;
    using static BookWorm.Common.ToastMessages;

    public class ReplyController : ForumBaseController
    {
        private readonly IReplyService replyService;
        private readonly IForumPostService forumPostService;
        private readonly IToastNotification toastNotification;

        public ReplyController(
            IReplyService replyService,
            IForumPostService forumPostService,
            IToastNotification toastNotification)
        {
            this.replyService = replyService;
            this.forumPostService = forumPostService;
            this.toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            ReplyFormViewModel model = new ReplyFormViewModel()
            {
                PostId = id,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReplyFormViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                string? userId = User.GetUserId();
                if (userId == null) BadRequest();

                bool postExists = await forumPostService.ForumPostExistsAsync(id);
                if (!postExists) return NotFound();


                await replyService.AddReplyAsync(model, userId!);

                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "reply"));
                return View();
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
