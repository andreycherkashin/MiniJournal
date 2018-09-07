using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infotecs.MiniJournal.WcfService.DataTransferObjects
{
    /// <summary>
    /// Комментарий к статье.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Уникальный идентификатор комментария.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Пользователь создавший комментарий.
        /// </summary>
        public User User { get; set; }
    }
}