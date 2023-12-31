﻿namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Review;
    using BookWorm.Web.Infrastructure.ExtensionMethods;
    using BookWorm.Services.Models.Review;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;
    using BookWorm.Data.Models;
    using Microsoft.Extensions.Caching.Memory;
    using Ganss.Xss;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IPoemService poemService;
        private readonly IBookService bookService;
        private readonly IToastNotification toastNotification;
        private readonly IMemoryCache memoryCache;
        private readonly IHtmlSanitizer sanitizer;

        public ReviewController(
            IReviewService reviewService,
            IPoemService poemService,
            IBookService bookService,
            IToastNotification toastNotification,
            IMemoryCache memoryCache,
            IHtmlSanitizer sanitizer)
        {
            this.reviewService = reviewService;
            this.poemService = poemService;
            this.bookService = bookService;
            this.toastNotification = toastNotification;
            this.memoryCache = memoryCache;
            this.sanitizer = sanitizer; 
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> AddPoem(string id)
        {
            try
            {
                string? userId = User.GetUserId();

                if (userId == null) return BadRequest();

                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return BadRequest();

                bool isDeleted = await poemService.IsPoemDeletedAsync(id);
                if (isDeleted) return NotFound();

                ViewData["Title"] = "Add Poem Review";
                ViewData["Action"] = "Submit";
                return View(new ReviewFormViewModel());
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddPoem(string id, ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                string? userId = User.GetUserId();
                if (userId == null) BadRequest();

                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId!, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return NotFound();

                bool isDeleted = await poemService.IsPoemDeletedAsync(id);
                if (isDeleted) return NotFound();

                model.Title = sanitizer.Sanitize(model.Title);
                model.Content = sanitizer.Sanitize(model.Content);

                model.PoemId = Guid.Parse(id);
                await reviewService.CreatePoemReviewAsync(userId!, model);

                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "review"));
                memoryCache.Remove(ReviewUserCache);
                memoryCache.Remove(ReviewMineCache);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> AddBook(int id)
        {
            try
            {
                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await bookService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                ViewData["Title"] = "Add Book Review";
                ViewData["Action"] = "Submit";
                return View(new ReviewFormViewModel());
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddBook(int id, ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }


            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await bookService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                model.Title = sanitizer.Sanitize(model.Title);
                model.Content = sanitizer.Sanitize(model.Content);

                model.BookId = id;
                await reviewService.CreateBookReviewAsync(userId!, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "review"));
                memoryCache.Remove(ReviewUserCache);
                memoryCache.Remove(ReviewMineCache);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditPoem(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool reviewExists = await reviewService.ExistsByIdAsync(id);
                if (!reviewExists) return NotFound();

                string? poemId = await reviewService.RetriveReviewPoemIdAsync(id);
                if (poemId == null) return BadRequest();

                bool poemExists = await poemService.ExistsByIdAsync(poemId);
                if (!poemExists) return BadRequest();

                bool isPoemDeleted = await poemService.IsPoemDeletedAsync(poemId);
                if (isPoemDeleted) return BadRequest();

                bool isOwner = await reviewService.IsUserReviewOwnerAsync(userId, id);

                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                bool isReviewDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isReviewDeleted) return NotFound();

                ReviewFormViewModel model = await reviewService.FindReviewByIdAsync(id);


                ViewData["Title"] = "Edit Poem Review";
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
        public async Task<IActionResult> EditPoem(string id, ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool reviewExists = await reviewService.ExistsByIdAsync(id);
                if (!reviewExists) return NotFound();

                string? poemId = await reviewService.RetriveReviewPoemIdAsync(id);
                if (poemId == null) return BadRequest();

                bool poemExists = await poemService.ExistsByIdAsync(poemId);
                if (!poemExists) return BadRequest();

                bool isPoemDeleted = await poemService.IsPoemDeletedAsync(poemId);
                if (isPoemDeleted) return BadRequest();

                bool isOwner = await reviewService.IsUserReviewOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                bool isReviewDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isReviewDeleted) return NotFound();

                model.Title = sanitizer.Sanitize(model.Title);
                model.Content = sanitizer.Sanitize(model.Content);

                await reviewService.EditReviewAsync(id, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyEditedItemMessage, "review"));
                memoryCache.Remove(ReviewUserCache);
                memoryCache.Remove(ReviewMineCache);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await reviewService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isDeleted) return NotFound();

                int? bookId = await reviewService.RetriveReviewBookIdAsync(id);
                if (bookId == null) return BadRequest();

                bool bookExists = await bookService.ExistsByIdAsync((int)bookId);
                if (!bookExists) return BadRequest();

                bool isBookDeleted = await bookService.IsDeletedAsync((int)bookId);
                if (isBookDeleted) return BadRequest();

                bool isOwner = await reviewService.IsUserReviewOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                ReviewFormViewModel model = await reviewService.FindReviewByIdAsync(id);

                ViewData["Title"] = "Edit Book Review";
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
        public async Task<IActionResult> EditBook(string id, ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await reviewService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isDeleted) return NotFound();

                int? bookId = await reviewService.RetriveReviewBookIdAsync(id);
                if (bookId == null) return BadRequest();

                bool bookExists = await bookService.ExistsByIdAsync((int)bookId);
                if (!bookExists) return BadRequest();

                bool isBookDeleted = await bookService.IsDeletedAsync((int)bookId);
                if (isBookDeleted) return BadRequest();

                bool isOwner = await reviewService.IsUserReviewOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                model.Title = sanitizer.Sanitize(model.Title);
                model.Content = sanitizer.Sanitize(model.Content);

                await reviewService.EditReviewAsync(id, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyEditedItemMessage, "review"));
                memoryCache.Remove(ReviewUserCache);
                memoryCache.Remove(ReviewMineCache);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await reviewService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isDeleted) return NotFound();

                bool isOwner = await reviewService.IsUserReviewOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                await reviewService.SoftDeleteReviewAsync(id);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyDeletedItemMessage, "review"));
                memoryCache.Remove(ReviewUserCache);
                memoryCache.Remove(ReviewMineCache);
                return RedirectToAction("Mine", "Review");
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
                if (userId == null) return NotFound();

                bool exists = await reviewService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isDeleted) return NotFound();

                ReviewDetailsViewModel model = await reviewService.GetReviewAsDetailsViewModelAsync(id);
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
                if (userId == null) return NotFound();

                IEnumerable<ReviewDisplayViewModel> model;

                model = memoryCache.Get<IEnumerable<ReviewDisplayViewModel>>(ReviewMineCache);
                if (model == null)
                {
                    model = await reviewService.GetAllUserReviewsAsync(userId);

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                    memoryCache.Set(ReviewMineCache, model, cacheOptions);
                }
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Like(string id, bool isDisliked)
        {
            try
            {
                bool exists = await reviewService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isDeleted) return NotFound();

                ReviewLikeServiceModel model = await reviewService.LikeReviewAsync(id, isDisliked);

                return Ok(model);

            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Dislike(string id, bool isLiked)
        {
            try
            {
                bool exists = await reviewService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isDeleted) return NotFound();

                ReviewLikeServiceModel model = await reviewService.DislikeReviewAsync(id, isLiked);

                return Ok(model);

            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
