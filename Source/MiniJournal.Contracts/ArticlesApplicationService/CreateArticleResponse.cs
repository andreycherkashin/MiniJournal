using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запроса создания статьи.
    /// </summary>
    public class CreateArticleResponse
    {
        public CreateArticleResponse()
        {    
        }

        public CreateArticleResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
