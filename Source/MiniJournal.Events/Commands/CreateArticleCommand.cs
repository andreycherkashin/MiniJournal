using System;

namespace Infotecs.MiniJournal.Events.Commands
{
    /// <summary>
    /// Создания статью.
    /// </summary>
    public class CreateArticleCommand : ICommand
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleCommand"/> class.
        /// </summary>
        public CreateArticleCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleCommand"/> class.
        /// </summary>
        /// <param name="text">Текст статьи.</param>
        /// <param name="image">Картинка.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        public CreateArticleCommand(string text, byte[] image, long userId)
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
