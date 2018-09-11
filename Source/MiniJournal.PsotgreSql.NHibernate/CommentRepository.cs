using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Comments;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    internal class CommentRepository : BaseNHibernateRepository, ICommentRepository
    {
        public CommentRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        /// <summary>
        /// Добавляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="comment">Комментарий.</param>
        public Task AddAsync(long articleId, Comment comment)
            => this.Session.SaveAsync(comment);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="comment">Комментарий.</param>
        public Task DeleteAsync(long articleId, Comment comment)
            => this.Session.DeleteAsync(comment);
    }
}
