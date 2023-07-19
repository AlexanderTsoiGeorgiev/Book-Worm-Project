namespace BookWorm.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BookWorm.Data.Models;

    public class PoemTableConfiguration : IEntityTypeConfiguration<Poem>
    {
        public void Configure(EntityTypeBuilder<Poem> builder)
        {
            builder
                .HasOne(p => p.Author)
                .WithMany(a => a.Poems)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
