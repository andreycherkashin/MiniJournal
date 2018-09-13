using System;

namespace Infotecs.MiniJournal.Contracts.Commands.ArticlesApplicationService
{
    /// <summary>
    /// Результат запроса удаления статьи.
    /// </summary>
    public class DeleteCommentResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommentResponse"/> class.
        /// </summary>
        public DeleteCommentResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommentResponse"/> class.
        /// </summary>
        /// <param name="success">Успешна ли операция.</param>
        public DeleteCommentResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Gets or sets a value indicating whether успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
