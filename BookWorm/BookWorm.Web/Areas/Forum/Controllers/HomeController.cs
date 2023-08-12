namespace BookWorm.Web.Areas.Forum.Controllers
{
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ForumBaseController
    {
        private ITagService tagService;

        public HomeController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public async Task<IActionResult> Index([FromQuery] ForumAllViewModel model)
        {
            model.Tags = await tagService.AllTagNamesAsync(); 
            return View(model);
        }
    }
}
