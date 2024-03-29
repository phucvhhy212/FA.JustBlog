﻿using FA.JustBlog.Core.Config;
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
                optionsBuilder.UseMySQL("server=xlf3ljx3beaucz9x.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;uid=yc8hracrqflw4tnw;pwd=jo7jnc9mdzadgh93;database=bgg1no4kjzdr5del");
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
