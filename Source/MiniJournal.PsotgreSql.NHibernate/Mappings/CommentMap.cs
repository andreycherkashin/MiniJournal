using System;
using FluentNHibernate.Mapping;
using Infotecs.MiniJournal.Domain.Comments;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings
{
    /// <inheritdoc />
    public class CommentMap : ClassMap<Comment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentMap"/> class.
        /// </summary>
        public CommentMap()
        {
            this.Table("comments");
            this.Id(x => x.Id, "id").GeneratedBy.Native();
            this.Map(x => x.Text, "text");

            this.References(x => x.User);
            this.References(x => x.Article);
        }
    }
}
