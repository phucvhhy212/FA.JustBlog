using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Core.Config
{
    public class CategoryConfig:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.UrlSlug).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1024);

        }
    }
}
