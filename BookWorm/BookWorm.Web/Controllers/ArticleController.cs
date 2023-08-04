namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Article;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IPoemService poemService;

        public ArticleController(IArticleService articleService, IPoemService poemService)
        {
            this.articleService = articleService;
            this.poemService = poemService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            return View(new ArticleFormViewModel());
        }

        [HttpPost]
        public IActionResult Add(string id, ArticleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }
    }
}
