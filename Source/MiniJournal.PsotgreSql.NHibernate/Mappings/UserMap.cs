using System;
using FluentNHibernate.Mapping;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings
{
    internal class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            this.Table("users");
            this.Id(x => x.Id, "id").GeneratedBy.Native();
            this.Map(x => x.Name, "name");
        }
    }
}
