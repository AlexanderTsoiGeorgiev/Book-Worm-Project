namespace BookWorm.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BookWorm.Data.Models;

    public class TagTableConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(SeedTags());
        }

        private Tag[] SeedTags()
        {
            int id = 0;
            List<Tag> tags = new List<Tag>();
            Tag tag;

            tag = new Tag()
            {
                Id = ++id,
                Name = "Question"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = ++id,
                Name = "Issue"
            };
            tags.Add(tag);

            return tags.ToArray();
        }
    }
}
