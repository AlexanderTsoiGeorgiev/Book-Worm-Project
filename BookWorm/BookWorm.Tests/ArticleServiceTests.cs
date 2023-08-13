namespace BookWorm.Tests
{
    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services;
    using BookWorm.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using static BookWorm.Data.Common.AuthorIds;

    public class ArticleServiceTests
    {
        private DbContextOptions<BookWormDbContext> dbOptions;
        private BookWormDbContext dbContext;

        private IArticleService articleService;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<BookWormDbContext>()
                .UseInMemoryDatabase("BookWormDbContext" + Guid.NewGuid().ToString())
                .Options;
            dbContext = new BookWormDbContext(dbOptions);

            dbContext.Database.EnsureCreated();
            SeedArticles();

            articleService = new ArticleService(dbContext);
        }

        [Test]
        public async Task TestExistsById()
        {
            string id = dbContext.Articles.Where(a => a.AuthorId == Guid.Parse(WilliamShakespeareId)).First().Id.ToString();

            bool result = await articleService.ExistsByIdAsync(id);

            Assert.IsTrue(result);
        }


        private void SeedArticles()
        {
            List<Article> articles = new List<Article>();
            Article article;

            article = new Article()
            {
                AuthorId = Guid.Parse(WilliamShakespeareId),
                Content = "content",
                DatePosted = DateTime.Now,
                DateEdited = DateTime.Now,
                IsDeleted = false,
                PoemId = Guid.NewGuid(),
                Poem = new Poem(),
                Author = new ApplicationUser() { FirstName = "asd", LastName = "asd" },
                Title = "title",
            };
            articles.Add(article);

            article = new Article()
            {
                AuthorId = Guid.Parse(EmilyDickinsonId),
                Content = "content",
                DatePosted = DateTime.Now,
                DateEdited = DateTime.Now,
                IsDeleted = false,
                PoemId = Guid.NewGuid(),
                Poem = new Poem(),
                Author = new ApplicationUser() { FirstName = "asd", LastName = "asd" },
                Title = "title",
            };
            articles.Add(article);

            article = new Article()
            {
                AuthorId = Guid.Parse(EdgarAllanPoeId),
                Content = "content",
                DatePosted = DateTime.Now,
                DateEdited = DateTime.Now,
                IsDeleted = false,
                PoemId = Guid.NewGuid(),
                Poem = new Poem(),
                Author = new ApplicationUser() { FirstName = "asd", LastName="asd"},
                Title = "title",
            };
            articles.Add(article);

            dbContext.AddRange(articles);
            dbContext.SaveChanges();

        }
    }
}
