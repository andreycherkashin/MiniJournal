using System;

namespace Infotecs.MiniJournal.Events.Commands
{
    /// <summary>
    /// Создать пользователя.
    /// </summary>
    public class CreateUserCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommand"/> class.
        /// </summary>
        public CreateUserCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommand"/> class.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        public CreateUserCommand(string userName)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Gets or sets имя нового пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
