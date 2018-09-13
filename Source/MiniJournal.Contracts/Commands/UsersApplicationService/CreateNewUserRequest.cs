using System;

namespace Infotecs.MiniJournal.Contracts.Commands.UsersApplicationService
{
    /// <summary>
    /// Запрос создания нового пользователя.
    /// </summary>
    public class CreateNewUserRequest : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewUserRequest"/> class.
        /// </summary>
        public CreateNewUserRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewUserRequest"/> class.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        public CreateNewUserRequest(string userName)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Gets or sets имя нового пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
