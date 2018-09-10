using System;

namespace Infotecs.MiniJournal.PostgreSql
{
    internal class PostgresStatementsProvider : IDbEngineStatementsProvider
    {
        public string GetLastInsertedIdSelectStatement() => "RETURNING id";
    }
}
