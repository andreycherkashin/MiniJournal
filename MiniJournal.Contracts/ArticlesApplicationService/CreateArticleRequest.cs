using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    public class CreateArticleRequest
    {
        public string Text { get; set; }        
        public byte[] Image { get; set; }
        public long UserId { get; set; }
    }
}
