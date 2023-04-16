using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Core.Config
{
    public class CommentConfig:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(x=>x.Post).WithMany(x=>x.Comments).HasForeignKey(x=>x.Id);
        }
    }
}
