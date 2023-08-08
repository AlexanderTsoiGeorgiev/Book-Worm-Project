namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Review;
    using BookWorm.Web.Infrastructure.ExtensionMethods;
    using static BookWorm.Common.ToastMessages;
    using BookWorm.Services.Models.Review;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IPoemService poemService;
        private readonly IBookService bookService;
        private readonly IToastNotification toastNotification;

        public ReviewController(
            IReviewService reviewService,
            IPoemService poemService,
            IBookService bookService,
            IToastNotification toastNotification)
        {
            this.reviewService = reviewService;
            this.poemService = poemService;
            this.bookService = bookService;
            this.toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Works
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

                model.PoemId = Guid.Parse(id);
                await reviewService.CreatePoemReviewAsync(userId!, model);

                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "review"));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }


        //Works, but add proper exceptions and error pages
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

                model.BookId = id;
                await reviewService.CreateBookReviewAsync(userId!, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "review"));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        //Works
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

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, poemId);
                bool isPrivete = await poemService.IsPoemPrivateAsync(poemId);

                if (isPrivete && !isOwner) return NotFound();

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

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, poemId);
                bool isPrivete = await poemService.IsPoemPrivateAsync(poemId);

                if (isPrivete && !isOwner) return NotFound();

                bool isReviewDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isReviewDeleted) return NotFound();

                await reviewService.EditReviewAsync(id, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyEditedItemMessage, "review"));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        //Semi-implemented
        //Test it
        //Add Exceptions
        [HttpGet]
        public async Task<IActionResult> EditBook(string id)
        {
            try
            {
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

                await reviewService.EditReviewAsync(id, model);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyEditedItemMessage, "review"));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        //TODO: Add Get method and add exceptions
        //TODO: Implement after finishing User Controller
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

                bool isUserOwner = await reviewService.IsUserReviewOwnerAsync(userId, id);
                if (isUserOwner) return BadRequest();

                await reviewService.SoftDeleteReviewAsync(id);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyDeletedItemMessage, "review"));
                return RedirectToAction("Mine", "Review");
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }


        //Implement this now
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

                bool isUserOwner = await reviewService.IsUserReviewOwnerAsync(userId, id);
                if (!isUserOwner) return BadRequest();

                ReviewDetailsViewModel model = await reviewService.GetReviewAsDetailsViewModelAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        //Implement this now
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return NotFound();

                IEnumerable<ReviewDisplayViewModel> model = await reviewService.GetAllUserReviewsAsync(userId);
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
