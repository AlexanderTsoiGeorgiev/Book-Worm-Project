namespace BookWorm.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BookWorm.Data.Models;

    public class BookPoemTableConfiguration : IEntityTypeConfiguration<BookPoem>
    {
        public void Configure(EntityTypeBuilder<BookPoem> builder)
        {
            builder.HasKey(bp => new { bp.PoemId, bp.BookId });
        }
    }
}
