using System;
using Dapper.FluentMap.Mapping;
using Infotecs.MiniJournal.Domain.Articles;

namespace Infotecs.MiniJournal.PostgreSql.DapperMappings
{
    internal class ArticleMap : EntityMap<Article>
    {
        internal ArticleMap()
        {
            this.Map(x => x.Id).ToColumn("id");
            this.Map(x => x.UserId).ToColumn("user_id");
            this.Map(x => x.ImageId).ToColumn("image_id");
            this.Map(x => x.Text).ToColumn("text");            
        }
    }
}
