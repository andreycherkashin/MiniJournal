using System;

namespace Infotecs.MiniJournal.PostgreSql
{
    internal interface IDbEngineStatementsProvider
    {
        string GetLastInsertedIdSelectStatement();
    }
}
