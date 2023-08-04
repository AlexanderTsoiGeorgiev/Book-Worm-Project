namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Article;
    using BookWorm.Web.Infrastructure.ExtensionMethods;
    using BookWorm.Services;

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

                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return BadRequest();

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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool articleExists = await articleService.ExistsByIdAsync(id);
                if (!articleExists) return NotFound();

                bool isUserOwner = await articleService.IsUserArticleOwner(userId, id);
                if (!isUserOwner) return BadRequest();

                bool isDeleted = await articleService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                ArticleFormViewModel model = await articleService.FindArticleAsArticleFormViewModelByIdAsync(id);

                ViewData["Title"] = "Edit Article";
                ViewData["Action"] = "Edit";
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ArticleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Edit Article";
                ViewData["Action"] = "Edit";
                return View(model);
            }
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool articleExists = await articleService.ExistsByIdAsync(id);
                if (!articleExists) return NotFound();

                bool isUserOwner = await articleService.IsUserArticleOwner(userId, id);
                if (!isUserOwner) return BadRequest();

                bool isDeleted = await articleService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                model.PoemId = await articleService.GetArticlePoemIdAsync(id);

                await articleService.EditArticleAsync(id, model);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Read(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null!) return BadRequest();

                bool articleExists = await articleService.ExistsByIdAsync(id);
                if (!articleExists) return NotFound();

                bool isDeleted = await articleService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                ArticleReadViewModel model = await articleService.FindArticleAsArticleReadViewModelByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                IEnumerable<ArticleDisplayViewModel> model = await articleService.GetAllUserArticlesAsync(userId);


                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await articleService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await articleService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isUserOwner = await articleService.IsUserArticleOwner(userId, id);
                if (!isUserOwner) return BadRequest();


                ArticleDetailsViewModel model = await articleService.FindArticleAsArticleDetailsViewModelByIdAsync(id);
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await articleService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await articleService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isUserOwner = await articleService.IsUserArticleOwner(userId, id);
                if (!isUserOwner) return BadRequest();


                await articleService.SoftDeleteAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
