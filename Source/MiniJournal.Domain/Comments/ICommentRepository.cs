using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Comments
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(long articleId, Comment comment);
        Task RemoveAsync(long articleId, Comment comment);
    }
}
