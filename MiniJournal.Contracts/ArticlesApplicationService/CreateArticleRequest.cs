using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Запрос создания статьи.
    /// </summary>
    public class CreateArticleRequest
    {
        /// <summary>
        /// Текст статьи.
        /// </summary>
        public string Text { get; set; }        

        /// <summary>
        /// Картинка-тизер статьи.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Идентификатор пользователя создавшего статью.
        /// </summary>
        public long UserId { get; set; }
    }
}
