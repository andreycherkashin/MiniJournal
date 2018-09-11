using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
{
    /// <summary>
    /// Запрос создания нового пользователя. 
    /// </summary>
    public class CreateNewUserRequest
    {
        public CreateNewUserRequest()
        {
        }

        public CreateNewUserRequest(string userName)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Имя нового пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
