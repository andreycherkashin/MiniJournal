using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MiniJournal.PostgreSql
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
