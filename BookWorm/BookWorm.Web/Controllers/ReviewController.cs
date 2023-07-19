namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Review;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //TODO: Check whether or not passing the id of the poem is necessary
        //Something is not right debug this.
        [HttpGet]
        public IActionResult AddPoem(string id)
        {
            ReviewFormViewModel model = new ReviewFormViewModel 
            { 
                PoemId = Guid.Parse(id) 
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPoem(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            try
            {
                await reviewService.CreatePoemReviewAsync(model);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
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

            try
            {
                await reviewService.CreatePoemReviewAsync(model);
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
            ReviewFormViewModel model;
            try
            {
                model = await reviewService.FindReviewById(id);
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
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
