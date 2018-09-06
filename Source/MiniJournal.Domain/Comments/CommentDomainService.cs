using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments.Exceptions;

namespace Infotecs.MiniJournal.Domain.Comments
{
    internal class CommentDomainService : ICommentDomainService
    {
        private readonly ICommentRepository commentRepository;

        public CommentDomainService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(Article article, Comment comment)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            await this.commentRepository.AddCommentAsync(article.Id, comment);
            article.Comments.Add(comment);
        }

        public Task<Comment> GetCommentById(Article article, long commentId)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            var comment = article.Comments.Find(c => c.Id == commentId);
            if (comment == null)
            {
                throw new CommentNotFoundException();
            }

            return Task.FromResult(comment);
        }

        public async Task DeleteCommentAsync(Article article, Comment comment)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));            

            await this.commentRepository.RemoveAsync(article.Id, comment);
            article.Comments.Remove(comment);
        }
    }
}
