using System;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
{
    /// <summary>
    /// Запрос пользователя по имени.
    /// </summary>
    public class GetUserByNameRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByNameRequest"/> class.
        /// </summary>
        public GetUserByNameRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByNameRequest"/> class.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        public GetUserByNameRequest(string userName)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Gets or sets имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
