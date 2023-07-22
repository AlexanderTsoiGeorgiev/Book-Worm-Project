namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Services.Models.Poem;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    public class PoemController : BaseController
    {
        private readonly IPoemService poemService;
        private readonly IReviewService reviewService;

        public PoemController(IPoemService poemService, IReviewService reviewService)
        {
            this.poemService = poemService;
            this.reviewService = reviewService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] PoemQueryViewModel model)
        {
            PoemAllFilteredServiceModel filteredPoems;
            try
            {
                filteredPoems = await poemService.GetAllPoemsFilteredAsync(model);

            }
            catch (Exception)
            {

                throw;
            }

            model.Poems = filteredPoems.Poems;
            model.PoemsCount = filteredPoems.AllPoemsCount;
            model.Categories = await poemService.GetAllCategoryNamesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Read(string id)
        {
            PoemReadViewModel model;
            try
            {
                model = await poemService.FindPoemReadModelByIdAsync(id);
                model.Reviews = await reviewService.GetAllPoemReviewsAsync(id);

            }
            catch (Exception)
            {

                throw;
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            PoemFormViemModel model = new PoemFormViemModel
            {
                Categories = await poemService.GetAllCategoriesAsync()
            };
            return View(model);
        }
        //TODO: fix not valid model state & add try catch when accessing the DB
        [HttpPost]
        public async Task<IActionResult> Add(PoemFormViemModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            string? authorId = User.GetUserId();
            if (authorId == null)
            {
                throw new Exception();
            }

            //add try catch
            await poemService.CreatePoemAsync(authorId, model);

            return RedirectToAction(nameof(Index), nameof(PoemController));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            PoemFormViemModel model;

            try
            {
                model = await poemService.FindPoemByIdAsync(id);
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
        [HttpPost]
        public IActionResult Edit(string id, PoemFormViemModel model)
        {
            return View();
        }


        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            IEnumerable<PoemDisplayViewModel>? poems;
            string? userId = User.GetUserId() ?? throw new Exception();
            try
            {
                poems = await poemService.GetAllUserPoemsAsync(userId);
            }
            catch (Exception)
            {

                throw;
            }

            return View(poems);
        }
    }
}
