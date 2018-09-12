using System;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запроса удаления статьи.
    /// </summary>
    public class DeleteArticleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleResponse"/> class.
        /// </summary>
        public DeleteArticleResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleResponse"/> class.
        /// </summary>
        /// <param name="success">Результат операции.</param>
        public DeleteArticleResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Gets or sets a value indicating whether успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
