using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Repositories
{
    public class CommentRepository:GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(JustBlogContext context):base(context) { }
        public void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            Comment c = new Comment
            {
                PostId = postId,
                Name = commentName,
                Email = commentEmail,
                CommentHeader = commentTitle,
                CommentText = commentBody
            };
            dbSet.Add(c);
            context.SaveChanges();
        }

        public IList<Comment> GetCommentsForPost(int postId)
        {
            return dbSet.Where(x=>x.PostId==postId).ToList();
        }

        public IList<Comment> GetCommentsForPost(Post post)
        {
            return dbSet.Include(x=>x.Post).Where(x => x.Post.Equals(post)).ToList();
        }
    }
}
