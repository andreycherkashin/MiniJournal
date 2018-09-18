using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Comments;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <inheritdoc cref="ICommentRepository" />
    internal class CommentRepository : BaseNHibernateRepository, ICommentRepository
    {
        /// <inheritdoc cref="BaseNHibernateRepository"/>
        public CommentRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        /// <inheritdoc />
        public Task<Comment> FindByIdAsync(long commentId)
        {
            return this.Session.GetAsync<Comment>(commentId);
        }

        /// <inheritdoc />
        public Task AddAsync(long articleId, Comment comment)
        {
            return this.Session.SaveAsync(comment);
        }

        /// <inheritdoc />
        public Task DeleteAsync(long articleId, Comment comment)
        {
            return this.Session.DeleteAsync(comment);
        }
    }
}
