namespace BookWorm.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;
    using Ganss.Xss;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Reply;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class ReplyController : ForumBaseController
    {
        private readonly IReplyService replyService;
        private readonly IForumPostService forumPostService;
        private readonly IToastNotification toastNotification;
        private readonly IHtmlSanitizer sanitizer;

        public ReplyController(
            IReplyService replyService,
            IForumPostService forumPostService,
            IToastNotification toastNotification,
            IHtmlSanitizer sanitizer)
        {
            this.replyService = replyService;
            this.forumPostService = forumPostService;
            this.toastNotification = toastNotification;
            this.sanitizer = sanitizer;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home", new {Area = ForumAreaName});
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
            model.PostId = id;
            if(model.PostId == null)
            {
                ModelState.AddModelError("Post Id is Null", "Post id can not be null");
            }

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

                model.Content = sanitizer.Sanitize(model.Content);

                await replyService.AddReplyAsync(model, userId!);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "reply"));
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName});
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
