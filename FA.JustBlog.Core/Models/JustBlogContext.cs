using FA.JustBlog.Core.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Models
{
    public class JustBlogContext:IdentityDbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<PostTagMap> PostTagsMap { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }

        public JustBlogContext()
        {

        }
        public JustBlogContext(DbContextOptions<JustBlogContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=192.168.0.1025;Database=BlogDb;User Id=sa;Password=123;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostTagMapConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TagConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfig).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentConfig).Assembly);
            modelBuilder.SeedData();
        }
    }
}
