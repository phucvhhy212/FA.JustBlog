using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Repositories
{
    public class PostRepository:GenericRepository<Post>,IPostRepository
    {
        public PostRepository(JustBlogContext context): base(context)
        {
            
        }

        public Post FindPost(int year, int month, string urlSlug)
        {
            return dbSet.FirstOrDefault(x =>
                x.PostedOn.Month == month && x.PostedOn.Year == year && x.UrlSlug.Equals(urlSlug));
        }

        public IList<Post> GetUnpublishedPosts()
        {
            return dbSet.Where(x => !x.Published).ToList();
        }

        public IList<Post> GetPublishedPosts()
        {
            return dbSet.Where(x => x.Published).ToList();
        }

        public IList<Post> GetLatestPost(int size)
        {
            return dbSet.OrderByDescending(x=>x.PostedOn).Take(size).ToList();
        }

        public IList<Post> GetPostsByMonth(DateTime monthYear)
        {
            return dbSet.Where(x=>x.PostedOn.Month  == monthYear.Month).ToList();
        }

        public int CountPostsForCategory(string category)
        {
            return dbSet.Include(p => p.Category).Count(x => x.Category.Name.Equals(category));
        }

        public IList<Post> GetPostsByCategory(string category)
        {
            return dbSet.Include(p=>p.Category).Include(p=>p.PostTags).Where(x => x.Category.Name.Equals(category)).ToList();
            
        }

        public int CountPostsForTag(string tag)
        {
            return dbSet.Include(x=>x.PostTags).Count(x => x.PostTags.Any(pt=>pt.Tag.Name == tag));
        }

        public IList<Post> GetPostsByTag(string tag)
        {
            return dbSet.Include(p => p.Category).Include(x => x.PostTags).Where(x => x.PostTags.Any(pt => pt.Tag.Name == tag)).ToList();
        }

        public IList<Post> GetMostViewedPost(int size)
        {
            return dbSet.OrderByDescending(x=>x.ViewCount).Take(size).ToList();
        }

        public IList<Post> GetHighestPosts(int size)
        {
            return dbSet.OrderByDescending(x => Math.Round(((decimal)x.TotalRate/x.RateCount),2)).Take(size).ToList();

        }
    }
}
