namespace BookWorm.Web.Areas.Forum.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ForumBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
