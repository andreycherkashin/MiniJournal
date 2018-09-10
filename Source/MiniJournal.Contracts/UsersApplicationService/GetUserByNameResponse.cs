using System;
using System.Collections.Generic;
using System.Text;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
{
    /// <summary>
    /// Результат запроса пользователя по имени.
    /// </summary>
    public class GetUserByNameResponse
    {
        public GetUserByNameResponse()
        {    
        }        

        public GetUserByNameResponse(User user)
        {
            this.User = user;
        }

        /// <summary>
        /// Найденный пользователь.
        /// </summary>
        public User User { get; set; }
    }
}
