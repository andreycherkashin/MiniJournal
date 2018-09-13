using System;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат добавления комментария к статье.
    /// </summary>
    public class AddCommentResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentResponse"/> class.
        /// </summary>
        public AddCommentResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentResponse"/> class.
        /// </summary>
        /// <param name="success">Результат операции.</param>
        public AddCommentResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Gets or sets a value indicating whether успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
