using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.WebApp.ApiControllers.Articles.Dto
{
    /// <summary>
    /// DTO для создания статьи.
    /// </summary>
    public class CreateArticle
    {
        /// <summary>
        /// Текст статьи.
        /// </summary>
        [Required]
        [MaxLength(10_000)]
        public string Text { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
    }
}
