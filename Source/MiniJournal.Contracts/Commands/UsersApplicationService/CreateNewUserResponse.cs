using System;

namespace Infotecs.MiniJournal.Contracts.Commands.UsersApplicationService
{
    /// <summary>
    /// Результат запроса создания нового пользователя.
    /// </summary>
    public class CreateNewUserResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewUserResponse"/> class.
        /// </summary>
        public CreateNewUserResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewUserResponse"/> class.
        /// </summary>
        /// <param name="success">Результат операции.</param>
        public CreateNewUserResponse(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Gets or sets a value indicating whether успешно ли выполнена операция.
        /// </summary>
        public bool Success { get; set; }
    }
}
