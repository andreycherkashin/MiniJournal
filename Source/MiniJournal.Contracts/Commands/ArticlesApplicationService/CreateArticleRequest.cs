using System;

namespace Infotecs.MiniJournal.Contracts.Commands.ArticlesApplicationService
{
    /// <summary>
    /// Запрос создания статьи.
    /// </summary>
    public class CreateArticleRequest : ICommand
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleRequest"/> class.
        /// </summary>
        public CreateArticleRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleRequest"/> class.
        /// </summary>
        /// <param name="text">Текст статьи.</param>
        /// <param name="image">Картинка.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        public CreateArticleRequest(string text, byte[] image, long userId)
        {
            this.Text = text;
            this.Image = image;
            this.UserId = userId;
        }

        /// <summary>
        /// Gets or sets текст статьи.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets картинка-тизер статьи.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets идентификатор пользователя создавшего статью.
        /// </summary>
        public long UserId { get; set; }
    }
}
