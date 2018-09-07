using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using Infotecs.MiniJournal.WcfService.DataTransferObjects;

namespace Infotecs.MiniJournal.WcfService
{    
    [ServiceContract]
    public interface IArticlesWebService
    {
        /// <summary>
        /// Возвращает список всех имеющихся статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        [OperationContract]
        Task<IEnumerable<Article>> GetArticlesAsync();

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="text">Содержимое статьи.</param>
        /// <param name="image">Картинка.</param>
        /// <param name="userId">Идентификатор пользователя создавшего статью.</param>
        [OperationContract]
        Task CreateArticleAsync(string text, byte[] image, long userId);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        [OperationContract]
        Task DeleteArticleAsync(long articleId);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="text">Содержимое комментария.</param>
        /// <param name="userId">Идентификатор пользователя добавившего комментарий.</param>
        /// <param name="articleId">Идентификатор статьи, к которой добавляется комментарий.</param>
        [OperationContract]
        Task AddCommentAsync(string text, long userId, long articleId);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        [OperationContract]
        Task DeleteCommentAsync(long articleId, long commentId);
    }    
}
