using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запроса удаления статьи.
    /// </summary>
    public class DeleteArticleResponse
    {
        public DeleteArticleResponse()
        {
        }

        public DeleteArticleResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
