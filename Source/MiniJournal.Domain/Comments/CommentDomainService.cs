using System;
using System.Linq;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments.Exceptions;

namespace Infotecs.MiniJournal.Domain.Comments
{
    /// <inheritdoc/>
    /// <summary>
    /// Действия над комментарием.
    /// </summary>
    internal class CommentDomainService : ICommentDomainService
    {
        private readonly ICommentRepository commentRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="CommentDomainService"/> class.
        /// </summary>
        /// <param name="commentRepository">Репозиторий комментариев.</param>
        public CommentDomainService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="article">Статья.</param>
        /// <param name="comment">Комментарий.</param>
        public async Task AddCommentAsync(Article article, Comment comment)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            await this.commentRepository.AddAsync(article.Id, comment);
            article.Comments.Add(comment);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Возвращает комментарий по идентификатору.
        /// </summary>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Comments.Exceptions.CommentNotFoundException">
        /// Если комментарий я таким идентификатором не найден.
        /// </exception>
        /// <param name="article">Статья.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <returns>Комментарий.</returns>
        public Task<Comment> GetCommentById(Article article, long commentId)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            Comment comment = article.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                throw new CommentNotFoundException();
            }

            return Task.FromResult(comment);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="article">Статья.</param>
        /// <param name="comment">Комментарий.</param>
        public async Task DeleteCommentAsync(Article article, Comment comment)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            await this.commentRepository.DeleteAsync(article.Id, comment);
            article.Comments.Remove(comment);
        }
    }
}
