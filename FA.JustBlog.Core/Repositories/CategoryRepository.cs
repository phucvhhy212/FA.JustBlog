using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repositories
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {

        public CategoryRepository(JustBlogContext context) : base(context)
        {
        }
    }
}
