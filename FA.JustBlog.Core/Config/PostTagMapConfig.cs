using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Core.Config
{
    public class PostTagMapConfig:IEntityTypeConfiguration<PostTagMap>
    {
        public void Configure(EntityTypeBuilder<PostTagMap> builder)
        {
            builder.HasKey(pt => new { pt.PostId, pt.TagId });
            builder.HasOne<Post>(p => p.Post).WithMany(p => p.PostTags).HasForeignKey(pt => pt.PostId);
            builder.HasOne<Tag>(p => p.Tag).WithMany(t => t.PostTags).HasForeignKey(pt => pt.TagId);
        }
    }
}
