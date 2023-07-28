﻿namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        private readonly IPoemService poemService;

        public BookController(IBookService bookService, IPoemService poemService)
        {
            this.bookService = bookService;
            this.poemService = poemService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //TODO: Add Exceptions & Redirects
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                BookFormViewModel model = new BookFormViewModel
                {
                    Poems = await poemService.GetUserPoemsAsPoemBookSelectViewModelAsync(userId),
                };

                bool hasPoems = await poemService.UserHasPoemsAsync(userId);
                if (!hasPoems) return RedirectToAction("Add", "Poem");

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }


        }
        [HttpPost]
        public async Task<IActionResult> Add(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                string? authorId = User.GetUserId();
                if (authorId == null) return BadRequest();

                bool userOwnsAllPoems = await bookService.DoesUserOwnAllPoemsAsync(authorId, model.PoemIds.ToArray());
                if (!userOwnsAllPoems) return BadRequest();

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
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return BadRequest();

                model = await bookService.FindBookByIdFormModelAsync(id);
                model.Poems = await poemService.GetUserPoemsAsPoemBookSelectViewModelAsync(userId);
                model.SelectedPoemsIds = await bookService.GetSelectedPoemIdsAsync(id);
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
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
                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();
                bool isUserOwner = await bookService.IsUserOwnerAsync(userId, id);
                if (!isUserOwner) return BadRequest();

                await bookService.EditBookAsync(id, model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        //TODO: Add exceptions and Get action
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool exists = await bookService.ExistsByIdAsync(id);
                if (!exists) return NotFound();

                bool isOwner = await bookService.IsUserOwnerAsync(userId, id);
                if (!isOwner) return BadRequest();

                await bookService.SoftDeleteBookAsync(id);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index");
        }
    }
}
