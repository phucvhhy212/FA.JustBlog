using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Core.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Title)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.UrlSlug).HasMaxLength(255);
            builder.Property(p=>p.ShortDescription).HasMaxLength(1024);
            builder.Property(p => p.RateCount).IsRequired();
            builder.Property(p => p.ViewCount).IsRequired();
            builder.Property(p => p.TotalRate).IsRequired();
            builder.Ignore(p => p.Rate); 
            builder.HasOne(p => p.Category).WithMany(p => p.Posts).HasForeignKey(c => c.CategoryId);
        }
    }
}
