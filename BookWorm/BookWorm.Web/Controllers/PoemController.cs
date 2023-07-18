namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Services.Interfaces;

    [Authorize]
    public class PoemController : Controller
    {
        private readonly IPoemService poemService;

        public PoemController(IPoemService poemService)
        {
            this.poemService = poemService;
        }

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

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
