using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    public class DeleteCommentRequest
    {
        public long ArticleId { get; set; }
        public long CommentId { get; set; }
    }
}
