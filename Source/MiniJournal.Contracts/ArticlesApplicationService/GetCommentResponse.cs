using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запроса комментария.
    /// </summary>
    public class GetCommentResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCommentResponse"/> class.
        /// </summary>
        public GetCommentResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCommentResponse"/> class.
        /// </summary>
        /// <param name="comment"><see cref="Comment"/>.</param>
        public GetCommentResponse(Comment comment)
        {
            this.Comment = comment;
        }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public Comment Comment { get; set; }
    }
}
