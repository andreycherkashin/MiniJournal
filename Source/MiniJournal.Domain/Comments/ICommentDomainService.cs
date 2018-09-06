using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;

namespace Infotecs.MiniJournal.Domain.Comments
{
    public interface ICommentDomainService
    {
        Task AddCommentAsync(Article article, Comment comment);
        Task DeleteCommentAsync(Article article, Comment comment);
        Task<Comment> GetCommentById(Article article, long commentId);
    }
}