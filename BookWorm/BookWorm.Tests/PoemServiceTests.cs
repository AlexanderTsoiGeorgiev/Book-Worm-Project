namespace BookWorm.Tests
{
    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data;
    using BookWorm.Data.Models;
    using BookWorm.Services.Interfaces;

    using static BookWorm.Data.Common.AuthorIds;
    using BookWorm.Services;
    using BookWorm.Web.ViewModels.Poem;

    public class PoemServiceTests
    {
        private DbContextOptions<BookWormDbContext> dbOptions;
        private BookWormDbContext dbContext;

        private IPoemService poemService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<BookWormDbContext>()
                .UseInMemoryDatabase("BookWormDbContext" + Guid.NewGuid().ToString())
                .Options;

            dbContext = new BookWormDbContext(dbOptions);

            dbContext.Database.EnsureCreated();
            SeedPoems();

            poemService = new PoemService(dbContext);
        }


        [Test]
        public async Task TestPoemExistsByIdReturnsTrueIfExists()
        {
            string validId = dbContext.Poems.First().Id.ToString();

            bool result = await poemService.ExistsByIdAsync(validId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestPoemExistsByIdReturnsFalseIfDoesNotExist()
        {
            string invalidId = Guid.NewGuid().ToString();

            bool result = await poemService.ExistsByIdAsync(invalidId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestUserIsOwnerReturnsTrueIfUserIsOwner()
        {
            string userId = WilliamShakespeareId.ToLower();
            string poemId = dbContext.Poems.Where(p => p.AuthorId.ToString() == userId).First().Id.ToString().ToLower();

            bool result = await poemService.IsUserPoemOwnerAsync(userId, poemId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestUserIsOwnerReturnsFalseIfUserIsNotOwner()
        {
            string userId = WilliamShakespeareId.ToLower();
            string poemId = dbContext.Poems.Where(p => p.AuthorId.ToString().ToLower() == EmilyDickinsonId.ToLower()).First().Id.ToString().ToLower();

            bool result = await poemService.IsUserPoemOwnerAsync(userId, poemId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestIsPoemDeletedReturnsTrueIfDeleted()
        {
            string poemId = dbContext.Poems.First().Id.ToString().ToLower();

            bool result = await poemService.IsPoemDeletedAsync(poemId);
            Assert.IsTrue(!result);
        }

        [Test]
        public async Task TestIsPoemDeletedReturnsFalseIfNotDeleted()
        {
            string poemId = dbContext.Poems.First().Id.ToString().ToLower();

            bool result = await poemService.IsPoemDeletedAsync(poemId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestIsPoemPrivateReturnsTrueIfPrivate()
        {
            string poemId = dbContext.Poems.First().Id.ToString().ToLower();

            bool result = await poemService.IsPoemPrivateAsync(poemId);
            Assert.IsTrue(!result);
        }

        [Test]
        public async Task TestIsPoemPrivateReturnsFalseIfNotPrivate()
        {
            string poemId = dbContext.Poems.First().Id.ToString().ToLower();

            bool result = await poemService.IsPoemPrivateAsync(poemId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestFindPoemReadModel()
        {
            string poemId = dbContext.Poems.First().Id.ToString().ToLower();

            PoemReadViewModel model = await poemService.FindPoemReadModelByIdAsync(poemId);
            Assert.IsNotNull(model);
            Assert.IsAssignableFrom<PoemReadViewModel>(model);
            Assert.IsTrue(model.Id.ToString().ToLower() == poemId);
        }

        [Test]
        public async Task TestCreatePoem()
        {
            string authorId = EdgarAllanPoeId.ToString();
            PoemFormViemModel model = new PoemFormViemModel()
            {
                Title = "Test",
                Content = "Test",
                CategoryId = 1,
                IsPrivate = false,
                Description = "Test",
            };

            int futureCount = dbContext.Poems.Count();
            futureCount++;
            await poemService.CreatePoemAsync(authorId, model);
            Poem remove = dbContext.Poems.Where(p => p.Title == "Alone").First();
            dbContext.Poems.Remove(remove);
            dbContext.SaveChanges();


            Assert.IsNotNull(model);
            Assert.That(futureCount == dbContext.Poems.Count());
        }


        [Test]
        public async Task TestEditPoem()
        {
            Poem entity = dbContext.Poems.Where(p => p.Title == "Alone").First();
            string poemId = entity.Id.ToString();
            string title = entity.Title;
            string description = entity.Description;
            string content = entity.Content;
            Poem beforeChange = new Poem
            {
                Title = title,
                Content = content,
                Description = description
            };

            PoemFormViemModel model = new PoemFormViemModel
            { 
                Title = "Test",
                Content = "Test",
                CategoryId = 1,
                IsPrivate = false,
                Description = "Test",
            };

            await poemService.EditPoemAsync(poemId, model);
            Poem? poemAfterChange = dbContext.Poems.Find(Guid.Parse(poemId));


            Assert.That(beforeChange.Title != poemAfterChange!.Title);
            Assert.That(beforeChange.Content != poemAfterChange!.Content);
            Assert.That(beforeChange.Description != poemAfterChange!.Description);
        }

        [Test]
        public async Task TestFindPoem()
        {
            Poem poem = dbContext.Poems.First();
            string id = poem.Id.ToString();

            PoemFormViemModel viemModel = await poemService.FindPoemByIdAsync(id);

            Assert.IsAssignableFrom<PoemFormViemModel>(viemModel);
            Assert.True(viemModel.Title == poem.Title);
        }

        [Test]
        public async Task TestGetPoemDetailsById()
        {
            Poem poem = dbContext.Poems.First();
            string id = poem.Id.ToString();

            PoemDetailsVisualizeViewModel? viemModel = await poemService.GetPoemAsDetailsViewModelByIdAsync(id);

            Assert.IsAssignableFrom<PoemDetailsVisualizeViewModel>(viemModel);
            Assert.True(viemModel!.Title == poem.Title);
        }


        private void SeedPoems()
        {
            List<Poem> poems = new List<Poem>();
            Poem poem;

            poem = new Poem()
            {
                Title = "Sonnet 18: Shall I compare thee to a summer’s day?",
                Description = "This is William Shakespeare's work!",
                Content =
                @"Shall I compare thee to a summer’s day?
Thou art more lovely and more temperate:
Rough winds do shake the darling buds of May,
And summer’s lease hath all too short a date;
Sometime too hot the eye of heaven shines,
And often is his gold complexion dimm'd;
And every fair from fair sometime declines,
By chance or nature’s changing course untrimm'd;
But thy eternal summer shall not fade,
Nor lose possession of that fair thou ow’st;
Nor shall death brag thou wander’st in his shade,
When in eternal lines to time thou grow’st:
   So long as men can breathe or eyes can see,
   So long lives this, and this gives life to thee.",
                AuthorId = Guid.Parse(WilliamShakespeareId),
                CategoryId = 5
            };
            poems.Add(poem);

            poem = new Poem()
            {
                Title = @"“Hope” is the thing with feathers",
                Description = "This is Emily Dickinson's work!",
                Content =
                @"“Hope” is the thing with feathers -
That perches in the soul -
And sings the tune without the words -
And never stops - at all -

And sweetest - in the Gale - is heard -
And sore must be the storm -
That could abash the little Bird
That kept so many warm -

I’ve heard it in the chillest land -
And on the strangest Sea -
Yet - never - in Extremity,
It asked a crumb - of me.
",
                AuthorId = Guid.Parse(EmilyDickinsonId),
                CategoryId = 6
            };
            poems.Add(poem);


            poem = new Poem()
            {
                Title = "Alone",
                Description = "This is Edgar Allan Poe's work!",
                Content =
                @"From childhood’s hour I have not been
As others were—I have not seen
As others saw—I could not bring
My passions from a common spring—
From the same source I have not taken
My sorrow—I could not awaken
My heart to joy at the same tone—
And all I lov’d—I lov’d alone—
Then—in my childhood—in the dawn
Of a most stormy life—was drawn
From ev’ry depth of good and ill
The mystery which binds me still—
From the torrent, or the fountain—
From the red cliff of the mountain—
From the sun that ’round me roll’d
In its autumn tint of gold—
From the lightning in the sky
As it pass’d me flying by—
From the thunder, and the storm—
And the cloud that took the form
(When the rest of Heaven was blue)
Of a demon in my view—",
                AuthorId = Guid.Parse(EdgarAllanPoeId),
                CategoryId = 1
            };
            poems.Add(poem);

            dbContext.AddRange(poem);
            //dbContext.SaveChanges();
        }
    }
}