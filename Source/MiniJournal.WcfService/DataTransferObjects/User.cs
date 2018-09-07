using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infotecs.MiniJournal.WcfService.DataTransferObjects
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
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