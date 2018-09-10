using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
{
    /// <summary>
    /// Результат запроса создания нового пользователя.
    /// </summary>
    public class CreateNewUserResponse
    {
        /// <summary>
        /// Успешно ли выполнена операция
        /// </summary>
        public bool Success { get; set; }
    }
}
