using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;

namespace FA.JustBlog.Core.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private bool disposed = false;
        private readonly JustBlogContext context;
        public UnitOfWork(JustBlogContext context=null)
        {
            this.context = context ?? new JustBlogContext( );
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize( this );

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                    Console.WriteLine("Dispose");

                }
            }
            this.disposed = true;
        }

        private ICategoryRepository _categoryRepository;
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;
        private ICommentRepository _commentRepository;

        public IPostRepository PostRepository => _postRepository ?? new PostRepository(context);
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(context);
        public ITagRepository TagRepository => _tagRepository ?? new TagRepository(context);

        public ICommentRepository CommentRepository => _commentRepository ?? new CommentRepository(context);

        public void SaveChange()
        {
            context.SaveChanges();
        }
    }
}
