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
            try
            {
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();
                model.Poems = await poemService.GetUserPoemsAsPoemBookSelectViewModelAsync(userId);
            }
            catch (Exception)
            {

                throw;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                string? authorId = User.GetUserId();
                if (authorId == null) return BadRequest();
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

        //TODO: Add exceptions and Get action
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
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
