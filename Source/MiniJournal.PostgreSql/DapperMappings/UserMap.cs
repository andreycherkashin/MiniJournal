using System;
using System.Collections.Generic;
using System.Text;
using Dapper.FluentMap.Mapping;
using Infotecs.MiniJournal.Domain.Users;

namespace MiniJournal.PostgreSql.DapperMappings
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
