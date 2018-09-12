using System;
using FluentNHibernate.Mapping;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings
{
    /// <inheritdoc />
    internal class UserMap : ClassMap<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            this.Table("users");
            this.Id(x => x.Id, "id").GeneratedBy.Native();
            this.Map(x => x.Name, "name");
        }
    }
}
