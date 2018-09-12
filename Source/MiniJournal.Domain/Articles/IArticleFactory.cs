using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Articles
{
    /// <summary>
    /// Инкапсулирует процесс и способ создания статьи.
    /// </summary>
    public interface IArticleFactory
    {
        /// <summary>
        /// Создает статью.
        /// </summary>
        /// <param name="text">Содержимое статьи.</param>
        /// <param name="imageId">Идентификатор картинки.</param>
        /// <param name="user">Пользователь создавший комментарий.</param>
        /// <returns>Созданный комментарий.</returns>
        Task<Article> CreateAsync(string text, string imageId, User user);
    }
}
