﻿using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles.Exceptions;

namespace Infotecs.MiniJournal.Domain.Articles
{
    /// <summary>
    /// Класс для работы со статьей.
    /// </summary>
    public interface IArticleDomainService
    {
        /// <summary>
        /// Создает статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task CreateArticleAsync(Article article);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task DeleteArticleAsync(Article article);

        /// <summary>
        /// Возвращает статью по идентификатору.
        /// </summary>
        /// <exception cref="ArticleNotFoundException">
        /// Если статья не найдена.
        /// </exception>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <returns>Найденную статью.</returns>
        Task<Article> GetArticleByIdAsync(long articleId);
    }
}
