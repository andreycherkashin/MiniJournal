using System;
using Dapper.FluentMap.Mapping;
using Infotecs.MiniJournal.Domain.Comments;

namespace Infotecs.MiniJournal.PostgreSql.DapperMappings
{
    internal class CommentMap : EntityMap<Comment>
    {
        internal CommentMap()
        {
            this.Map(x => x.Id).ToColumn("id");
            this.Map(x => x.ArticleId).ToColumn("article_id");
            this.Map(x => x.UserId).ToColumn("user_id");
            this.Map(x => x.Text).ToColumn("text");
        }
    }
}
