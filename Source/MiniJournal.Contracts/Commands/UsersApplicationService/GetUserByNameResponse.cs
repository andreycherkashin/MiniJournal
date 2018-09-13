using System;
using Infotecs.MiniJournal.Contracts.Commands.UsersApplicationService.Entities;

namespace Infotecs.MiniJournal.Contracts.Commands.UsersApplicationService
{
    /// <summary>
    /// Результат запроса пользователя по имени.
    /// </summary>
    public class GetUserByNameResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByNameResponse"/> class.
        /// </summary>
        public GetUserByNameResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByNameResponse"/> class.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public GetUserByNameResponse(User user)
        {
            this.User = user;
        }

        /// <summary>
        /// Gets or sets найденный пользователь.
        /// </summary>
        public User User { get; set; }
    }
}
