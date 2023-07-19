namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    public class PoemController : BaseController
    {
        private readonly IPoemService poemService;

        public PoemController(IPoemService poemService)
        {
            this.poemService = poemService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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

            //add try catch
            await poemService.CreatePoemAsync(model);

            return RedirectToAction(nameof(Index), nameof(PoemController));
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
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
