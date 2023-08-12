namespace BookWorm.Web.Controllers
{
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;
    using BookWorm.Web.ViewModels.Article;
    using BookWorm.Web.ViewModels.Book;
    using BookWorm.Web.ViewModels.Poem;
    using BookWorm.Web.ViewModels.Review;
    using BookWorm.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using NToastNotify;

    using static BookWorm.Common.ToastMessages;
    using static BookWorm.Common.GeneralApplicationConstants;

    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IToastNotification toastNotification;
        private readonly IMemoryCache memoryCache;

        public UserController(
            IUserService userService,
            IToastNotification toastNotification,
            IMemoryCache memoryCache
            )
        {
            this.userService = userService;
            this.toastNotification = toastNotification;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string userName)
        {
            try
            {
                ApplicationUser user = await userService.GetUserByUserNameAsync(userName);
                if (user == null)
                {
                    toastNotification.AddErrorToastMessage("User with the specified user name does not exist!");
                    return RedirectToAction("Index", "Home");
                }

                UserSearchDisplayViewModel model = new UserSearchDisplayViewModel
                {
                    UserId = user.Id.ToString(),
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Poems(string id)
        {
            try
            {
                ApplicationUser user = await userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    toastNotification.AddErrorToastMessage("User with the specified user name does not exist!");
                    return RedirectToAction("Index", "Home");
                }

                ViewData["Title"] = $"{user.FirstName} ({user.UserName}) {user.LastName}'s Poems";
                IEnumerable<PoemDisplayViewModel> model;
                model = memoryCache.Get<IEnumerable<PoemDisplayViewModel>>(PoemUserCache);
                if (model == null)
                {
                    model = await userService.GetUserPoemsAsDisplayModelAsync(id);

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                    memoryCache.Set(PoemUserCache, model, cacheOptions);

                }
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Books(string id)
        {
            try
            {
                ApplicationUser user = await userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    toastNotification.AddErrorToastMessage("User with the specified user name does not exist!");
                    return RedirectToAction("Index", "Home");
                }

                ViewData["Title"] = $"{user.FirstName} ({user.UserName}) {user.LastName}'s Books";
                IEnumerable<BookDisplayViewModel> model;
                model = memoryCache.Get<IEnumerable<BookDisplayViewModel>>(BookUserCache);
                if (model == null)
                {
                    model = await userService.GetUserBooksAsDisplayModelAsync(id);

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                    memoryCache.Set(BookUserCache, model, cacheOptions);

                }
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(string id)
        {
            try
            {
                ApplicationUser user = await userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    toastNotification.AddErrorToastMessage("User with the specified user name does not exist!");
                    return RedirectToAction("Index", "Home");
                }

                ViewData["Title"] = $"{user.FirstName} ({user.UserName}) {user.LastName}'s Reviews";
                IEnumerable<ReviewDisplayViewModel> model;

                model = memoryCache.Get<IEnumerable<ReviewDisplayViewModel>>(ReviewUserCache);

                if (model == null)
                {
                    model = await userService.GetUserReviewsAsDisplayModelAsync(id);

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                    memoryCache.Set(ReviewUserCache, model, cacheOptions);

                }

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Articles(string id)
        {
            try
            {
                ApplicationUser user = await userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    toastNotification.AddErrorToastMessage("User with the specified user name does not exist!");
                    return RedirectToAction("Index", "Home");
                }

                ViewData["Title"] = $"{user.FirstName} ({user.UserName}) {user.LastName}'s Articles";
                IEnumerable<ArticleDisplayViewModel> model;

                model = memoryCache.Get<IEnumerable<ArticleDisplayViewModel>>(ArticleUserCache);
                if (model == null)
                {
                    model = await userService.GetUserArticlesAsDisplayModelAsync(id);

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationMinutes));

                    memoryCache.Set(ArticleUserCache, model, cacheOptions);
                }

                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage(DatabaseErrorMessage);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
