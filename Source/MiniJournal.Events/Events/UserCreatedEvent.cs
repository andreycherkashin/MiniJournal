using System;

namespace Infotecs.MiniJournal.Events.Events
{
    /// <summary>
    /// Событие создания пользователя.
    /// </summary>
    public class UserCreatedEvent : IEvent

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedEvent"/> class.
        /// </summary>
        public UserCreatedEvent()
        {
            this.DateOfCreate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedEvent"/> class.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        public UserCreatedEvent(long userId, string userName)
            : this()
        {
            this.UserId = userId;
            this.UserName = userName;
        }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <inheritdoc />
        public DateTime DateOfCreate { get; }
    }
}
