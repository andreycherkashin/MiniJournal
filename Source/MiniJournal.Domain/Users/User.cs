using System;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public User(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        protected User()
        {
        }

        /// <summary>
        /// Gets or sets идентификатор пользователя.
        /// </summary>
        public virtual long Id { get; protected set; }

        /// <summary>
        /// Gets or sets имя пользователя.
        /// </summary>
        public virtual string Name { get; protected set; }
    }
}
