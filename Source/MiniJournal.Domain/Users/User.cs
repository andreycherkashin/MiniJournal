using System;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        protected User()
        {
        }

        /// <summary>
        /// Создает пользователя на основе имени.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public User(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Идентификатор пользователя. 
        /// </summary>
        public virtual long Id { get; protected set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public virtual string Name { get; protected set; }
    }
}
