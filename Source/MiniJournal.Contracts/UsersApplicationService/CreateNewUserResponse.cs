using System;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
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
        /// <param name="userId">Идентификатор созданного пользователя.</param>
        public CreateNewUserResponse(long userId)
        {
            this.UserId = userId;
        }

        /// <summary>
        /// Идентификатор созданного пользователя.
        /// </summary>
        public long UserId { get; set; }
    }
}
