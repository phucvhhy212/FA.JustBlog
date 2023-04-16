using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repositories
{
    public interface ICommentRepository:IGenericRepository<Comment>
    {
        void AddComment(int postId, string commentName, string commentEmail, string commentTitle,
            string commentBody);
        IList<Comment> GetCommentsForPost(int postId);
        IList<Comment> GetCommentsForPost(Post post);
    }
}
