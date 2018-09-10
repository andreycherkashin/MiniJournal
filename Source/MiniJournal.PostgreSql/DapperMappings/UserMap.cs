using System;
using Dapper.FluentMap.Mapping;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.PostgreSql.DapperMappings
{
    internal class UserMap : EntityMap<User>
    {
        internal UserMap()
        {
            this.Map(x => x.Id).ToColumn("id");
            this.Map(x => x.Name).ToColumn("name");
        }
    }
}
