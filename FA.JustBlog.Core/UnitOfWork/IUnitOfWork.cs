using FA.JustBlog.Core.Repositories;

namespace FA.JustBlog.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        ITagRepository TagRepository { get; }

        ICommentRepository CommentRepository { get; }

        void SaveChange();

    }

}
