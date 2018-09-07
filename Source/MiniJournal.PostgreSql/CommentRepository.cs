﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Infotecs.MiniJournal.Domain.Comments;

namespace MiniJournal.PostgreSql
{
    internal class CommentRepository : ICommentRepository
    {
        private readonly IDbConnectionFactory connectionFactory;

        public CommentRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task AddCommentAsync(long articleId, Comment comment)
        {
            using (var connection = this.connectionFactory.Create())
            {
                await connection.ExecuteAsync(
                    "INSERT INTO comments (article_id, user_id, text) VALUES (@ArticleId, @UserId, @Text)",
                    new { ArticleId = articleId, UserId = comment.User.Id, comment.Text });
            }
        }

        public async Task RemoveAsync(long articleId, Comment comment)
        {
            using (var connection = this.connectionFactory.Create())
            {
                await connection.ExecuteAsync(
                    "DELETE FROM comments WHERE id = @CommentId AND article_id = @ArticleId",
                    new { ArticleId = articleId, Commentid = comment.Id });
            }
        }
    }
}