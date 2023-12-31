﻿namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using Ganss.Xss;
    using NToastNotify;

    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        private readonly IPoemService poemService;
        private readonly IToastNotification toastNotification;
        private readonly IMemoryCache memoryCache;
        private readonly IHtmlSanitizer sanitizer;

        public BookController(
            IBookService bookService,
            IPoemService poemService,
            IToastNotification toastNotification,
            IMemoryCache memoryCache,
            IHtmlSanitizer sanitizer)
        {
            this.bookService = bookService;
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
        public async Task<IActionResult> Add()
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                BookFormViewModel model = new BookFormViewModel
                {
                    Poems = await poemService.GetUserPoemsAsPoemBookSelectViewModelAsync(userId),
                };

                bool hasPoems = await poemService.UserHasPoemsAsync(userId);
                if (!hasPoems) return RedirectToAction("Add", "Poem");

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }


        }
        [HttpPost]
        public async Task<IActionResult> Add(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }
            try
            {
                string? authorId = User.GetUserId();
                if (authorId == null) return BadRequest();

                bool userOwnsAllPoems = await bookService.DoesUserOwnAllPoemsAsync(authorId, model.PoemIds.ToArray());
                if (!userOwnsAllPoems) return BadRequest();

                model.Title = sanitizer.Sanitize(model.Title);
                model.Description = sanitizer.Sanitize(model.Description);
                model.ImageUrl = sanitizer.Sanitize(model.ImageUrl);

                await bookService.CreateBookAsync(authorId, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "book"));
                memoryCache.Remove(BookUserCache);
                memoryCache.Remove(BookMineCache);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookFormViewModel model;
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isOwner = await bookService.IsUserOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                bool isDeleted = await bookService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();


                model = await bookService.FindBookByIdFormModelAsync(id);
                model.Poems = await poemService.GetUserPoemsAsPoemBookSelectViewModelAsync(userId);
                model.SelectedPoemsIds = await bookService.GetSelectedPoemIdsAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool isOwner = await bookService.IsUserOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                bool isDelete = await bookService.IsDeletedAsync(id);
                if (isDelete) return NotFound();

                model.Title = sanitizer.Sanitize(model.Title);
                model.Description = sanitizer.Sanitize(model.Description);
                model.ImageUrl = sanitizer.Sanitize(model.ImageUrl);

                await bookService.EditBookAsync(id, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "book"));
                memoryCache.Remove(BookUserCache);
                memoryCache.Remove(BookMineCache);
                return RedirectToAction(nameof(Index));
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

                IEnumerable<BookDisplayViewModel>? model;

                model = memoryCache.Get<IEnumerable<BookDisplayViewModel>>(BookMineCache);
                if (model == null)
                {
                    model = await bookService.GetAllUserBooksAsync(userId);
                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                    memoryCache.Set(BookMineCache, model, cacheOptions);
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
        public async Task<IActionResult> Read(int id, [FromQuery] BookReadViewModel model)
        {
            try
            {
                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await bookService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                BookReadViewModel placeholder = await bookService.GetBookAsBookReadModelAsync(id);
                model.Id = placeholder.Id;
                model.Title = placeholder.Title;
                model.AuthorUserName = placeholder.AuthorUserName;
                model.Poems = await bookService.GetBookPoemsAsPoemBookReadModelAsync(id);
                model.Reviews = await bookService.GetBookReviewsAsReviewDisplayViewModel(id);

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isOwner = await bookService.IsUserOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                await bookService.SoftDeleteBookAsync(id);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyDeletedItemMessage, "book"));
                memoryCache.Remove(BookUserCache);
                memoryCache.Remove(BookMineCache);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
