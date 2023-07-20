namespace BookWorm.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using BookWorm.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryTableConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private Category[] SeedCategories()
        {
            ICollection<Category> categories = new List<Category>();
            int id = 1;

            Category category;

            category = new Category()
            {
                Id = id++,
                Name = "Poem"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = id++,
                Name = "Lyric Poety"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = id++,
                Name = "Elegy"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = id++,
                Name = "Ode"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = id++,
                Name = "Sonnet"
            };
            categories.Add(category);
            
            return categories.ToArray();
        }

    }
}
