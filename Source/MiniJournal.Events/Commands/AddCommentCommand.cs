using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Events.Commands
{
    /// <summary>
    /// Добавить комментарий к статье.
    /// </summary>
    public class AddCommentCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentCommand"/> class.
        /// </summary>
        public AddCommentCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentCommand"/> class.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="text">Текст комментария.</param>
        public AddCommentCommand(long userId, long articleId, string text)
        {
            this.UserId = userId;
            this.ArticleId = articleId;
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets идентификатор пользователя.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// Gets or sets текст комментария.
        /// </summary>
        public string Text { get; set; }
    }
}
