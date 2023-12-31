﻿namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using NToastNotify;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Article;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;
    using Ganss.Xss;

    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IPoemService poemService;
        private readonly IToastNotification toastNotification;
        private readonly IMemoryCache memoryCache;
        private readonly IHtmlSanitizer sanitizer;

        public ArticleController(
            IArticleService articleService,
            IPoemService poemService,
            IToastNotification toastNotification,
            IMemoryCache memoryCache,
            IHtmlSanitizer sanitizer)
        {
            this.articleService = articleService;
            this.poemService = poemService;
            this.toastNotification = toastNotification;
            this.memoryCache = memoryCache;
            this.sanitizer = sanitizer;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await poemService.IsPoemDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return NotFound();

                ViewData["Title"] = "Add an Article";
                ViewData["Action"] = "Add";
                return View(new ArticleFormViewModel());
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
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
                
                model.Title = sanitizer.Sanitize(model.Title);
                model.Content = sanitizer.Sanitize(model.Content);

                model.PoemId = Guid.Parse(id);
                await articleService.CreateArticleAsync(userId!, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "article"));
                memoryCache.Remove(ArticleUserCache);
                memoryCache.Remove(ArticleMineCache);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
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
                if (!(isUserOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                bool isDeleted = await articleService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                ArticleFormViewModel model = await articleService.FindArticleAsArticleFormViewModelByIdAsync(id);

                ViewData["Title"] = "Edit Article";
                ViewData["Action"] = "Edit";
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
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
                if (!(isUserOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                bool isDeleted = await articleService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                model.Title = sanitizer.Sanitize(model.Title);
                model.Content = sanitizer.Sanitize(model.Content);

                model.PoemId = await articleService.GetArticlePoemIdAsync(id);

                await articleService.EditArticleAsync(id, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyEditedItemMessage, "article"));
                memoryCache.Remove(ArticleUserCache);
                memoryCache.Remove(ArticleMineCache);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

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
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                IEnumerable<ArticleDisplayViewModel> model;

                model = memoryCache.Get<IEnumerable<ArticleDisplayViewModel>>(ArticleMineCache);

                if (model == null)
                {
                    model = await articleService.GetAllUserArticlesAsync(userId);

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                    memoryCache.Set(ArticleMineCache, model, cacheOptions);
                }


                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
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


                ArticleDetailsViewModel model = await articleService.FindArticleAsArticleDetailsViewModelByIdAsync(id);
                return View(model);

            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
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
                if (!(isUserOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();


                await articleService.SoftDeleteAsync(id);
                memoryCache.Remove(ArticleUserCache);
                memoryCache.Remove(ArticleMineCache);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
