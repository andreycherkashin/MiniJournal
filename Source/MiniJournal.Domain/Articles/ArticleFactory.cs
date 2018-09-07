using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Articles
{
    /// <inheritdoc />
    /// <summary>
    /// Инкапсулирует процесс и способ создания статьи.
    /// </summary>
    internal class ArticleFactory : IArticleFactory
    {
        /// <inheritdoc />
        /// <summary>
        /// Создает статью. 
        /// </summary>
        /// <param name="text">Содержимое статьи.</param>
        /// <param name="imageId">Идентификатор картинки.</param>
        /// <param name="user">Пользователь создавший комментарий.</param>
        /// <returns>Созданный комментарий.</returns>
        public Task<Article> CreateArticleAsync(string text, string imageId, User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var article = new Article(user, text, new List<Comment>());

            return Task.FromResult(article);
        }
    }
}
