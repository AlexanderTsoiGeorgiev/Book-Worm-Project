namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyPoems()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyBooks()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyReviews()
        {
            return View();
        }
    }
}
