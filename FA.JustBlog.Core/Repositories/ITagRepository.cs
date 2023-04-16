using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repositories
{
    public interface ITagRepository:IGenericRepository<Tag>
    {
        Tag GetTagByUrlSlug(string urlSlug);
    }
}
