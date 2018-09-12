using System;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entities
{
    /// <summary>
    /// Комментарий к статье.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets уникальный идентификатор комментария.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets содержимое комментария.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets пользователь создавший комментарий.
        /// </summary>
        public User User { get; set; }
    }
}
