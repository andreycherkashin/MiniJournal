using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    public class AddCommentRequest
    {
        public long UserId { get; set; }
        public long ArticleId { get; set; }
        public string Text { get; set; }
    }
}
