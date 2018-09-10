using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniJournal.PostgreSql
{
    internal class PostgresStatementsProvider : IDbEngineStatementsProvider
    {
        public string GetLastInsertedIdSelectStatement() => "RETURNING id";
    }
}
