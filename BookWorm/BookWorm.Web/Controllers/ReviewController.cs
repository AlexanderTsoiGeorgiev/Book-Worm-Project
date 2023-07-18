namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ReviewController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
