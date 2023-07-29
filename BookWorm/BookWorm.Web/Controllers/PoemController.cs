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


        //Add generic view
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Works
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] PoemQueryViewModel model)
        {
            PoemAllFilteredServiceModel filteredPoems;
            try
            {
                filteredPoems = await poemService.GetAllPoemsFilteredAsync(model);
                model.Poems = filteredPoems.Poems;
                model.PoemsCount = filteredPoems.AllPoemsCount;
                model.Categories = await poemService.GetAllCategoryNamesAsync();

            }
            catch (Exception)
            {

                throw;
            }


            return View(model);
        }

        //Works
        [HttpGet]
        public async Task<IActionResult> Read(string id)
        {
            PoemReadViewModel model;
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();
                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return NotFound();
                bool isDeleted = await poemService.IsPoemDeletedAsync(id);
                if (isDeleted) return BadRequest();
                bool isPrivate = await poemService.IsPoemPrivateAsync(id);
                bool isUserOwner = await poemService.IsUserPoemOwnerAsync(userId!, id);
                if (isPrivate && !isUserOwner) return BadRequest();

                model = await poemService.FindPoemReadModelByIdAsync(id);
                model.Reviews = await reviewService.GetAllPoemReviewsAsync(id);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Works
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

            return RedirectToAction(nameof(All));
        }

        //Works
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists)
                {
                    return BadRequest();
                }
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();
                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                if (!isOwner)
                {
                    return BadRequest();
                }

                PoemFormViemModel model = await poemService.FindPoemByIdAsync(id);
                model.Categories = await poemService.GetAllCategoriesAsync();

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, PoemFormViemModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists)
                {
                    return BadRequest();
                }
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();
                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                if (!isOwner)
                {
                    return BadRequest();
                }

                await poemService.EditPoemAsync(id, model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Decide whether or not an author which is not the owner should be able to view the details page
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            PoemDetailsVisualizeViewModel? model;
            try
            {
                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists)
                {
                    return BadRequest();
                }
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();
                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                if (!isOwner) return BadRequest();


                model = await poemService.GetPoemAsDetailsViewModelByIdAsync(id);

                if (model == null) return BadRequest(); //this check might be unnecessary

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }


        }

        //Figure out how to work with post method
        //It is done by using onclick js function combined with fetch api
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                bool poemExists = await poemService.ExistsByIdAsync(id);
                if (!poemExists)
                {
                    return BadRequest();
                }
                string? userId = User.GetUserId();
                if (userId == null)
                {
                    return BadRequest();
                }
                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                if (!isOwner)
                {
                    return BadRequest();
                }
                await poemService.SoftDeletePoemAsync(id);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Not implemented
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            IEnumerable<PoemDisplayViewModel>? poems;
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                poems = await poemService.GetAllUserPoemsAsync(userId);
                return View(poems);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
