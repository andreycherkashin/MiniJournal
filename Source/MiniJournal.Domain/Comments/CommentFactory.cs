using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Comments
{
    /// <inheritdoc />
    /// <summary>
    /// Инкапсулирует процесс и способ создания комментариев.
    /// </summary>
    internal class CommentFactory : ICommentFactory
    {
        /// <inheritdoc />
        /// <summary>
        /// Создает комментарий.
        /// </summary>
        /// <param name="text">Содержимое комментария.</param>
        /// <param name="user">Пользователь.</param>
        /// <param name="article">Статья.</param>
        /// <returns>Созданный комментарий.</returns>
        public Task<Comment> CreateAsync(string text, User user, Article article)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (article == null)
                throw new ArgumentNullException(nameof(article));

            var comment = new Comment(user, article, text);

            return Task.FromResult(comment);
        }
    }
}
