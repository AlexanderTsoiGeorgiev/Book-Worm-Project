namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        [HttpGet]
        public IActionResult Index(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string id)
        {
            return RedirectToAction(nameof(Index));
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

        [HttpGet]
        public IActionResult MyArticles()
        {
            return View();
        }
    }
}
