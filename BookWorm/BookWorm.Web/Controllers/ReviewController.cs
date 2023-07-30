namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Review;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IPoemService poemService;
        private readonly IBookService bookService;

        public ReviewController(IReviewService reviewService, IPoemService poemService, IBookService bookService)
        {
            this.reviewService = reviewService;
            this.poemService = poemService;
            this.bookService = bookService;
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
            }
            catch (Exception)
            {

                throw;
            }

            return View(new ReviewFormViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddPoem(string id, ReviewFormViewModel model)
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
                await reviewService.CreatePoemReviewAsync(userId!, model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }


        //Not implemented
        //Do these two after completing the book controller
        [HttpGet]
        public async Task<IActionResult> AddBook(int id)
        {
            try
            {
                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await bookService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();
            }
            catch (Exception)
            {

                throw;
            }


            return View(new ReviewFormViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(int id, ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }


            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isDeleted = await bookService.IsDeletedAsync(id);
                if (isDeleted) return NotFound();

                await reviewService.CreatePoemReviewAsync(userId!, model);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
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


                string? poemId = await reviewService.RetrivePoemIdAsync(id);
                if (poemId == null) return BadRequest();
                bool poemExists = await poemService.ExistsByIdAsync(poemId);
                if (!poemExists) return BadRequest();
                bool isPoemDeleted = await poemService.IsPoemDeletedAsync(poemId);
                if (isPoemDeleted) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, poemId);
                bool isPrivete = await poemService.IsPoemPrivateAsync(poemId);

                if (isPrivete && !isOwner) return BadRequest();

                bool isReviewDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isReviewDeleted) return NotFound();

                ReviewFormViewModel model = await reviewService.FindReviewByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditPoem(string id, ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool reviewExists = await reviewService.ExistsByIdAsync(id);
                if (!reviewExists) return NotFound();


                string? poemId = await reviewService.RetrivePoemIdAsync(id);
                if (poemId == null) return BadRequest();
                bool poemExists = await poemService.ExistsByIdAsync(poemId);
                if (!poemExists) return BadRequest();
                bool isPoemDeleted = await poemService.IsPoemDeletedAsync(poemId);
                if (isPoemDeleted) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, poemId);
                bool isPrivete = await poemService.IsPoemPrivateAsync(poemId);

                if (isPrivete && !isOwner) return BadRequest();

                bool isReviewDeleted = await reviewService.IsReviewDeletedAsync(id);
                if (isReviewDeleted) return NotFound();

                await reviewService.EditReviewAsync(id, model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Not Implemented
        [HttpGet]
        public IActionResult EditBook(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditBook(int id, ReviewFormViewModel model)
        {
            return View();
        }

        //TODO: Add Get method and add exceptions
        //TODO: Implement after finishing User Controller
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await reviewService.SoftDeleteReviewAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
    }
}
