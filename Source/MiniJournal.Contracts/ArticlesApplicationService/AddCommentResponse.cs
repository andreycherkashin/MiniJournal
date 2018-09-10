using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат добавления комментария к статье.
    /// </summary>
    public class AddCommentResponse
    {
        public AddCommentResponse()
        {
        }

        public AddCommentResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
