using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
{
    /// <summary>
    /// Запрос пользователя по имени.
    /// </summary>
    public class GetUserByNameRequest
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UseName { get; set; }
    }
}
