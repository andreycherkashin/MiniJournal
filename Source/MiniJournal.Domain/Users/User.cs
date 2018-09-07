using System;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
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
        public long Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
