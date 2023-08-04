namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Article;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

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
        public async Task<IActionResult> Add(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool isDeleted = await poemService.IsPoemDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return BadRequest();

                ViewData["Title"] = "Add an Article";
                ViewData["Action"] = "Add";
                return View(new ArticleFormViewModel());
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add(string id, ArticleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) BadRequest();

                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId!, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return BadRequest();

                bool isDeleted = await poemService.IsPoemDeletedAsync(id);
                if (isDeleted) return NotFound();

                model.PoemId = Guid.Parse(id);
                await articleService.CreateArticleAsync(userId!, model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
