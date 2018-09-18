using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.WebApp.ApiControllers.Comments.Dto
{
    /// <summary>
    /// DTO добавления комментария.
    /// </summary>
    public class AddComment
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }
    }
}
