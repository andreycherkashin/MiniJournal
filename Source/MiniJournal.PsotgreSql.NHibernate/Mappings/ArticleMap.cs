using System;
using FluentNHibernate.Mapping;
using Infotecs.MiniJournal.Domain.Articles;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings
{
    internal class ArticleMap : ClassMap<Article>
    {
        public ArticleMap()
        {
            this.Table("articles");
            this.Id(x => x.Id, "id").GeneratedBy.Native();
            this.Map(x => x.Text, "text");

            this.References(x => x.User);
            this.HasMany(x => x.Comments).Cascade.Delete();
        }
    }
}
