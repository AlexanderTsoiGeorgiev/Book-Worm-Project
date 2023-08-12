namespace BookWorm.Web.Areas.Forum.Views.Home.Components
{
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Mvc;
    using NToastNotify;
    using static BookWorm.Common.ToastMessages;

    public class SearchBarViewComponent : ViewComponent
    {
        private IForumPostService forumPostService;
        private IToastNotification toastNotification;

        public SearchBarViewComponent(
            IForumPostService forumPostService, 
            IToastNotification toastNotification)
        {
            this.forumPostService = forumPostService;
            this.toastNotification = toastNotification;
        }

        public async Task<IViewComponentResult> InvokeAsync([FromQuery] ForumAllViewModel model)
        {
            try
            {
                IEnumerable<ForumDisplayViewModel> result = await forumPostService.SortedForumPostsAsync(model);
                model.Forums = result;

            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
            }
            return View(model);
        }
    }
}
