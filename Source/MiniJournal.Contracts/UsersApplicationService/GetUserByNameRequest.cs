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
        public GetUserByNameRequest()
        {
        }

        public GetUserByNameRequest(string userName)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
