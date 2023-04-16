using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repositories
{
    public class TagRepository:GenericRepository<Tag>,ITagRepository

    {
        
        public TagRepository(JustBlogContext context):base(context) { }

        public Tag GetTagByUrlSlug(string urlSlug)
        {
            return dbSet.FirstOrDefault(t => t.UrlSlug.Equals(urlSlug));
        }
    }
}
