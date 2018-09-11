using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Добавить комментарий к статье.
    /// </summary>
    public class AddCommentRequest
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public AddCommentRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public AddCommentRequest(long userId, long articleId, string text)
        {
            this.UserId = userId;
            this.ArticleId = articleId;
            this.Text = text;
        }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Text { get; set; }
    }
}
