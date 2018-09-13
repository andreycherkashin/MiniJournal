using System;

namespace Infotecs.MiniJournal.Events.Commands
{
    /// <summary>
    /// Создать пользователя.
    /// </summary>
    public class CreateNewUserCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewUserCommand"/> class.
        /// </summary>
        public CreateNewUserCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewUserCommand"/> class.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        public CreateNewUserCommand(string userName)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Gets or sets имя нового пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
