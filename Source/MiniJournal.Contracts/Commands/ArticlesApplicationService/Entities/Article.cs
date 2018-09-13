using System;
using System.Collections.Generic;
using Infotecs.MiniJournal.Contracts.Commands.UsersApplicationService.Entities;

namespace Infotecs.MiniJournal.Contracts.Commands.ArticlesApplicationService.Entities
{
    /// <summary>
    /// Статьи с комментариями.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Gets or sets уникальный идентификатор статьи.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets содержимое статьи.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets уникальный идентификатор картинки.
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// Gets or sets пользователь, создавший статью.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets список комментариев к статье.
        /// </summary>
        public List<Comment> Comments { get; set; }
    }
}
