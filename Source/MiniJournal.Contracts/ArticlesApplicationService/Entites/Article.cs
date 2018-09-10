using System;
using System.Collections.Generic;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entites
{
    /// <summary>
    /// Статьи с комментариями
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Уникальный идентификатор статьи
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Содержимое статьи
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Уникальный идентификатор картинки 
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// Пользователь, создавший статью.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Список комментариев к статье.
        /// </summary>
        public List<Comment> Comments { get; set; }
    }
}