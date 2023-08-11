﻿namespace BookWorm.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NToastNotify;

    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Services.Models.Poem;
    using BookWorm.Web.Infrastructure.ExtensionMethods;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class PoemController : BaseController
    {
        private readonly IPoemService poemService;
        private readonly IReviewService reviewService;
        private readonly IToastNotification toastNotification;

        public PoemController(
            IPoemService poemService,
            IReviewService reviewService,
            IToastNotification toastNotification
            )
        {
            this.poemService = poemService;
            this.reviewService = reviewService;
            this.toastNotification = toastNotification;
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

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
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
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        //Works
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                PoemFormViemModel model = new PoemFormViemModel
                {
                    Categories = await poemService.GetAllCategoriesAsync()
                };
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }
        //TODO: fix not valid model state & add try catch when accessing the DB
        [HttpPost]
        public async Task<IActionResult> Add(PoemFormViemModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                string? authorId = User.GetUserId();
                if (authorId == null) return BadRequest();

                await poemService.CreatePoemAsync(authorId, model);

                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyAddedItemMessage, "poem"));
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        //Works
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return BadRequest();
                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();
                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                PoemFormViemModel model = await poemService.FindPoemByIdAsync(id);
                model.Categories = await poemService.GetAllCategoriesAsync();

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, PoemFormViemModel model)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddWarningToastMessage(WarningFulfillFormRequirementsMessage);
                return View(model);
            }

            try
            {
                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return BadRequest();

                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                await poemService.EditPoemAsync(id, model);

                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyEditedItemMessage, "poem"));
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            PoemDetailsVisualizeViewModel? model;
            try
            {
                bool exists = await poemService.ExistsByIdAsync(id);
                if (!exists) return BadRequest();

                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                model = await poemService.GetPoemAsDetailsViewModelByIdAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                bool poemExists = await poemService.ExistsByIdAsync(id);
                if (!poemExists) return BadRequest();

                string? userId = User.GetUserId();
                if (userId == null) return BadRequest();

                bool isOwner = await poemService.IsUserPoemOwnerAsync(userId, id);
                if (!(isOwner || User.IsInRole(AdminRoleName) || User.IsInRole(ModeratorRoleName))) return BadRequest();

                await poemService.SoftDeletePoemAsync(id);
                toastNotification.AddSuccessToastMessage(String.Format(SuccesfullyDeletedItemMessage, "poem"));
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
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
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
