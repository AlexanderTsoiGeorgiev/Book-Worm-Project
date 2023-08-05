namespace BookWorm.Data.Configuration
{
    using BookWorm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReplyTableConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder
                .HasOne(r => r.ForumPost)
                .WithMany(fp => fp.Replies)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Author)
                .WithMany(a => a.Replies)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
