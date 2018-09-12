using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Infotecs.MiniJournal.RabbitMqClient;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;
using Infotecs.MiniJournal.WpfClient.Properties;
using RawRabbit.Exceptions;
using Serilog;
using AddCommentRequest = Infotecs.MiniJournal.Contracts.ArticlesApplicationService.AddCommentRequest;
using CreateArticleRequest = Infotecs.MiniJournal.Contracts.ArticlesApplicationService.CreateArticleRequest;
using CreateNewUserRequest = Infotecs.MiniJournal.Contracts.UsersApplicationService.CreateNewUserRequest;
using GetUserByNameRequest = Infotecs.MiniJournal.Contracts.UsersApplicationService.GetUserByNameRequest;
using GetUserByNameResponse = Infotecs.MiniJournal.Contracts.UsersApplicationService.GetUserByNameResponse;
using User = Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities.User;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <summary>
    /// Модель состояния главного экрана формы.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IArticlesServiceRabbitMqClient articlesServiceRabbitMqClient;
        private ICommand addArticleCommand;
        private ICommand addCommentCommand;
        private byte[] articleImage;
        private ObservableCollection<Article> articles;
        private string articleText;
        private string articleUser;
        private string commentText;
        private string commentUser;
        private ICommand loadArticlesCommand;
        private Article selectedArticle;
        private byte[] selectedArticleImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="articlesServiceRabbitMqClient">Класс длы размещения зада в очереди.</param>
        public MainWindowViewModel(IArticlesServiceRabbitMqClient articlesServiceRabbitMqClient)
        {
            this.articlesServiceRabbitMqClient = articlesServiceRabbitMqClient;
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Список статей.
        /// </summary>
        public ObservableCollection<Article> Articles
        {
            get => this.articles;

            set
            {
                this.articles = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбранная статья.
        /// </summary>
        public Article SelectedArticle
        {
            get => this.selectedArticle;

            set
            {
                this.selectedArticle = value;
                this.OnPropertyChanged();

                Task.Run(() =>
                {
                    string imageId = this.SelectedArticle?.ImageId;
                    if (imageId != null)
                    {
                        using (var serviceClient = new ArticlesWebServiceClient())
                        {
                            FindImageResponse response = serviceClient.FindImage(new FindImageRequest() { ImageId = imageId });
                            this.SelectedArticleImage = response.Image;
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Картинка к выбранной статье.
        /// </summary>
        public byte[] SelectedArticleImage
        {
            get => this.selectedArticleImage;

            set
            {
                this.selectedArticleImage = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Пользователь нового комментария.
        /// </summary>
        public string CommentUser
        {
            get => this.commentUser;

            set
            {
                this.commentUser = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текст нового комментария.
        /// </summary>
        public string CommentText
        {
            get => this.commentText;

            set
            {
                this.commentText = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текст новой статьи.
        /// </summary>
        public string ArticleText
        {
            get => this.articleText;

            set
            {
                this.articleText = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Пользователь новой статьи.
        /// </summary>
        public string ArticleUser
        {
            get => this.articleUser;

            set
            {
                this.articleUser = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Картинка новой статьи.
        /// </summary>
        public byte[] ArticleImage
        {
            get => this.articleImage;

            set
            {
                this.articleImage = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда добавления нового комментария.
        /// </summary>
        public ICommand AddCommentCommand
        {
            get
            {
                if (this.addCommentCommand == null)
                {
                    this.addCommentCommand = new RelayCommand(
                        _ => !string.IsNullOrWhiteSpace(this.CommentUser) && !string.IsNullOrWhiteSpace(this.CommentText),
                        async _ => await this.AddComment());
                }

                return this.addCommentCommand;
            }
        }

        /// <summary>
        /// Команда добавления новой статьи.
        /// </summary>
        public ICommand AddArticleCommand
        {
            get
            {
                if (this.addArticleCommand == null)
                {
                    this.addArticleCommand = new RelayCommand(
                        _ => !string.IsNullOrWhiteSpace(this.ArticleUser) && !string.IsNullOrWhiteSpace(this.ArticleText) && this.ArticleImage != null,
                        async _ => await this.AddArticle());
                }

                return this.addArticleCommand;
            }
        }

        /// <summary>
        /// Команда загрузки статей.
        /// </summary>
        public ICommand LoadArticlesCommand
        {
            get
            {
                if (this.loadArticlesCommand == null)
                {
                    this.loadArticlesCommand = new RelayCommand(_ => true, async _ => await this.LoadArticles());
                }

                return this.loadArticlesCommand;
            }
        }

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Название свойства, которое изменилось.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoadArticles()
        {
            Article previouslySelectedArticle = this.SelectedArticle;

            using (var serviceClient = new ArticlesWebServiceClient())
            {
                GetArticlesResponse response = await serviceClient.GetArticlesAsync(new GetArticlesRequest());
                this.Articles = new ObservableCollection<Article>(response.Articles);
            }

            this.SelectedArticle = this.Articles.FirstOrDefault(x => x.Id == previouslySelectedArticle?.Id);
        }

        private async Task AddComment()
        {
            string userName = this.CommentUser;
            string text = this.CommentText;
            long articleId = this.SelectedArticle.Id;

            this.CommentUser = null;
            this.CommentText = null;

            User user = await this.GetUser(userName);

            await this.articlesServiceRabbitMqClient.AddCommentAsync(new AddCommentRequest(user.Id, articleId, text));
        }

        private async Task AddArticle()
        {
            string userName = this.ArticleUser;
            string text = this.ArticleText;
            byte[] image = this.ArticleImage;

            this.ArticleUser = null;
            this.ArticleText = null;
            this.ArticleImage = null;

            User user = await this.GetUser(userName);

            await this.articlesServiceRabbitMqClient.CreateArticleAsync(new CreateArticleRequest(text, image, user.Id));
        }

        private async Task<User> GetUser(string userName)
        {
            User user;
            try
            {
                GetUserByNameResponse response = await this.articlesServiceRabbitMqClient.GetUserByNameAsync(new GetUserByNameRequest(userName));
                user = response.User;
            }
            catch (MessageHandlerException ex) when (ex.InnerMessage == "User not found.")
            {
                Log.Information(ex, "creating new user");

                await this.articlesServiceRabbitMqClient.CreateNewUserAsync(new CreateNewUserRequest(userName));

                // ждем секунду чтобы создался пользователь
                await Task.Delay(1000);

                GetUserByNameResponse response = await this.articlesServiceRabbitMqClient.GetUserByNameAsync(new GetUserByNameRequest(userName));
                user = response.User;
            }

            return user;
        }
    }
}
