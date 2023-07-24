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

        //TODO: Check whether or not passing the id of the poem is necessary
        //Something is not right debug this.
        [HttpGet]
        public async Task<IActionResult> AddPoem(string id)
        {
            try
            {
                string? userId = User.GetUserId();

                if (userId == null) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }

            ReviewFormViewModel model = new ReviewFormViewModel 
            { 
                PoemId = Guid.Parse(id) 
            };
            return View(model);
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

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId!, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return BadRequest();

                await reviewService.CreatePoemReviewAsync(userId!, model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult AddBook(int id)
        {
            ReviewFormViewModel model = new ReviewFormViewModel
            {
                BookId = id
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            string? userId = User.GetUserId();
            if (userId == null) return BadRequest();

            try
            {
                await reviewService.CreatePoemReviewAsync(userId!, model);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        //TODO: Add exceptions and redirects
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                string? userId = User.GetUserId();

                if (userId == null) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                bool isPrivete = await poemService.IsPoemPrivateAsync(id);

                if (isPrivete && !isOwner) return BadRequest();

                bool exists = await reviewService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                ReviewFormViewModel model = await reviewService.FindReviewByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await reviewService.EditReviewAsync(id, model);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }


        //TODO: Add Get method and add exceptions
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
