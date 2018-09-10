using Infotecs.MiniJournal.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
{
    /// <summary>
    /// Результат запроса пользователя по имени.
    /// </summary>
    public class GetUserByNameResponse
    {
        /// <summary>
        /// Найденный пользователь.
        /// </summary>
        public User User { get; set; }
    }
}
