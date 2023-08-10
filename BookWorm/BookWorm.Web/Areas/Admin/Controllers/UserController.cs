namespace BookWorm.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
