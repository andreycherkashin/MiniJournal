using System;

namespace Infotecs.MiniJournal.Contracts.Commands.UsersApplicationService.Entities
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets идентификатор пользователя.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets имя пользователя.
        /// </summary>
        public string Name { get; set; }
    }
}
