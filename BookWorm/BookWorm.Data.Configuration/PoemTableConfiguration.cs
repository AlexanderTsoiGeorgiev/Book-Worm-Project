namespace BookWorm.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BookWorm.Data.Models;
    using static BookWorm.Data.Common.AuthorIds;


    public class PoemTableConfiguration : IEntityTypeConfiguration<Poem>
    {
        public void Configure(EntityTypeBuilder<Poem> builder)
        {
            builder
                .HasOne(p => p.Author)
                .WithMany(a => a.Poems)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedPoems());
        }

        private Poem[] SeedPoems()
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

            return poems.ToArray();
        }
    }
}
