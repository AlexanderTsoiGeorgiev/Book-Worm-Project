namespace BookWorm.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;

    using BookWorm.Web.ViewModels.Tag;
    using BookWorm.Services.Interfaces;

    using static BookWorm.Common.GeneralApplicationConstants;
    using static BookWorm.Common.ToastMessages;
    using BookWorm.Web.ViewModels.Forum;
    using BookWorm.Web.Infrastructure.ExtensionMethods;
    using BookWorm.Data.Models;

    public class MessageController : ForumBaseController
    {
        private readonly ITagService tagService;
        private readonly IToastNotification toastNotification;
        private readonly IForumPostService forumPostService;
        private readonly IReplyService replyService;


        public MessageController(
            IForumPostService forumPostService,
            ITagService tagService,
            IToastNotification toastNotification,
            IReplyService replyService)
        {
            this.forumPostService = forumPostService;
            this.tagService = tagService;
            this.toastNotification = toastNotification;
            this.replyService= replyService;
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
                IEnumerable<TagDisplayViewModel> tags = await tagService.NonDeletedTagsAsDisplayModelAsync();
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
        public async Task<IActionResult> Add(ForumFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();


                await forumPostService.AddForumPostAsync(userId, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "forum post"));
                return RedirectToAction("Index", "Home", new { area = ForumAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                string? userId = User.GetUserId();

                bool postExists = await forumPostService.ForumPostExistsAsync(id);
                if (!postExists) return NotFound();

                bool isDeleted = await forumPostService.IsPostDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isUserOwner = await forumPostService.IsUserOwnerAsync(userId!, id);
                if (!(isUserOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                IEnumerable<TagDisplayViewModel> tags = await tagService.NonDeletedTagsAsDisplayModelAsync();
                ForumFormViewModel model = await forumPostService.FindForumPostAsForumFormModelAsync(id);
                model.Tags = tags;
                return View(model);

            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ForumFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {

                string? userId = User.GetUserId();

                bool postExists = await forumPostService.ForumPostExistsAsync(id);
                if (!postExists) return NotFound();

                bool tagExists = await tagService.TagExistsByIdAsync(model.TagId);
                if (!tagExists) return BadRequest();

                bool isTagDeleted = await tagService.IsTagDeleted(model.TagId);
                if (isTagDeleted) return BadRequest();

                bool isDeleted = await forumPostService.IsPostDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isUserOwner = await forumPostService.IsUserOwnerAsync(userId!, id);
                if (!(isUserOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                await forumPostService.EditForumPostAsync(id, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyEditedItemMessage, "forum post"));
                return RedirectToAction("Index", "Home", new { area = ForumAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Read(string id)
        {
            try
            {
                bool exists = await forumPostService.ForumPostExistsAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await forumPostService.IsPostDeletedAsync(id);
                if (isDeleted) return NotFound();

                ForumReadViewModel model = await forumPostService.GetForumAsReadViewModelAsync(id);
                model.Replies = await replyService.GetAllPostReplyAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                string? userId = User.GetUserId();

                bool exists = await forumPostService.ForumPostExistsAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await forumPostService.IsPostDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isUserOwner = await forumPostService.IsUserOwnerAsync(userId!, id);

                if (!(isUserOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return NotFound();

                ForumDetailsViewModel model = await forumPostService.GetForumAsDetailsModelAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {

                string? userId = User.GetUserId();

                bool postExists = await forumPostService.ForumPostExistsAsync(id);
                if (!postExists) return NotFound();

                bool isDeleted = await forumPostService.IsPostDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isUserOwner = await forumPostService.IsUserOwnerAsync(userId!, id);
                if (!(isUserOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                await forumPostService.SoftDeletePostAsync(id);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyDeletedItemMessage, "forum post"));
                return RedirectToAction("Index", "Home", new { area = ForumAreaName });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home", new { Area = ForumAreaName });
            }
        }
    }
}
