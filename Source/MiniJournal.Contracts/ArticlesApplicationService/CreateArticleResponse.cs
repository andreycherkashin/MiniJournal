using System;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запроса создания статьи.
    /// </summary>
    public class CreateArticleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleResponse"/> class.
        /// </summary>
        public CreateArticleResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleResponse"/> class.
        /// </summary>
        /// <param name="success">Результат операции.</param>
        public CreateArticleResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Gets or sets a value indicating whether успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
