﻿using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Comments
{
    /// <summary>
    /// Инкапсулирует процесс и способ создания комментариев.
    /// </summary>
    public interface ICommentFactory
    {
        /// <summary>
        /// Создает комментарий.
        /// </summary>
        /// <param name="text">Содержимое комментария.</param>
        /// <param name="user">Пользователь.</param>
        /// <param name="article">Статья.</param>
        /// <returns>Созданный комментарий.</returns>
        Task<Comment> CreateAsync(string text, User user, Article article);
    }
}
