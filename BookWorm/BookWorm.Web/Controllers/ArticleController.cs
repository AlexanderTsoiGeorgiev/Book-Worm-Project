namespace BookWorm.Web.Controllers
{
    using BookWorm.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
