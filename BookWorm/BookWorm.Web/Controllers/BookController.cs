namespace BookWorm.Web.Controllers
{
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.Infrastructure.ExtensionMethods;
    using BookWorm.Web.ViewModels.Book;
    using Microsoft.AspNetCore.Mvc;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //TODO: Add Exceptions & Redirects
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? authorId = User.GetUserId();
            if (authorId == null)
            {
                throw new Exception();
            }

            try
            {
                await bookService.CreateBookAsync(authorId, model);

            }
            catch (Exception)
            {

                throw;
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookFormViewModel model;
            try
            {
                model = await bookService.FindBookByIdFormModelAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await bookService.EditBookAsync(id, model);
            }
            catch (Exception)
            {

                throw;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
