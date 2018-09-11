﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Infotecs.MiniJournal.RabbitMqClient;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;
using Infotecs.MiniJournal.WpfClient.Properties;
using Serilog;

namespace Infotecs.MiniJournal.WpfClient
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IArticlesServiceRabbitMqClient articlesServiceRabbitMqClient;
        private ObservableCollection<Article> articles;
        private Article selectedArticle;
        private string commentUser;
        private string commentText;
        private string articleText;
        private string articleUser;
        private ICommand addCommentCommand;
        private ICommand addArticleCommand;
        private ICommand loadArticlesCommand;
        private byte[] articleImage;
        private byte[] selectedArticleImage;

        public MainWindowViewModel(IArticlesServiceRabbitMqClient articlesServiceRabbitMqClient)
        {
            this.articlesServiceRabbitMqClient = articlesServiceRabbitMqClient;
        }

        public ObservableCollection<Article> Articles
        {
            get => this.articles;

            set
            {
                this.articles = value;
                this.OnPropertyChanged();
            }
        }

        public Article SelectedArticle
        {
            get => this.selectedArticle;

            set
            {
                this.selectedArticle = value;
                this.OnPropertyChanged();

                Task.Run(() =>
                {
                    var imageId = this.SelectedArticle?.ImageId;
                    if (imageId != null)
                    {
                        using (var serviceClient = new ArticlesWebServiceClient())
                        {
                            var response = serviceClient.FindImage(new FindImageRequest() { ImageId = imageId });
                            this.SelectedArticleImage = response.Image;
                        }
                    }
                });
            }
        }

        public byte[] SelectedArticleImage
        {
            get => this.selectedArticleImage;

            set
            {
                this.selectedArticleImage = value;
                this.OnPropertyChanged();
            }
        }

        public string CommentUser
        {
            get => this.commentUser;

            set
            {
                this.commentUser = value;
                this.OnPropertyChanged();
            }
        }

        public string CommentText
        {
            get => this.commentText;

            set
            {
                this.commentText = value;
                this.OnPropertyChanged();
            }
        }

        public string ArticleText
        {
            get => this.articleText;

            set
            {
                this.articleText = value;
                this.OnPropertyChanged();
            }
        }

        public string ArticleUser
        {
            get => this.articleUser;

            set
            {
                this.articleUser = value;
                this.OnPropertyChanged();
            }
        }

        public byte[] ArticleImage
        {
            get => this.articleImage;

            set
            {
                this.articleImage = value;
                this.OnPropertyChanged();
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoadArticles()
        {
            var previouslySelectedArticle = this.SelectedArticle;

            using (var serviceClient = new WcfServiceClient.ArticlesServiceReference.ArticlesWebServiceClient())
            {
                var response = await serviceClient.GetArticlesAsync(new GetArticlesRequest());                
                this.Articles = new ObservableCollection<Article>(response.Articles);
            }

            this.SelectedArticle = this.Articles.FirstOrDefault(x => x.Id == previouslySelectedArticle?.Id);
        }

        private async Task AddComment()
        {
            var userName = this.CommentUser;
            var text = this.CommentText;
            var articleId = this.SelectedArticle.Id;

            this.CommentUser = null;
            this.CommentText = null;

            var user = await this.GetUser(userName);

            await this.articlesServiceRabbitMqClient.AddCommentAsync(new Contracts.ArticlesApplicationService.AddCommentRequest(user.Id, articleId, text));
        }

        private async Task AddArticle()
        {
            var userName = this.ArticleUser;
            var text = this.ArticleText;
            byte[] image = this.ArticleImage;

            this.ArticleUser = null;
            this.ArticleText = null;
            this.ArticleImage = null;    

            var user = await this.GetUser(userName);

            await this.articlesServiceRabbitMqClient.CreateArticleAsync(new Contracts.ArticlesApplicationService.CreateArticleRequest(text, image, user.Id));
        }

        private async Task<Contracts.UsersApplicationService.Entities.User> GetUser(string userName)
        {

            Contracts.UsersApplicationService.Entities.User user;
            try
            {
                var response = await this.articlesServiceRabbitMqClient.GetUserByNameAsync(new Contracts.UsersApplicationService.GetUserByNameRequest(userName));
                user = response.User;
            }
            catch (RawRabbit.Exceptions.MessageHandlerException ex) when (ex.InnerMessage == "User not found.")
            {
                Log.Information(ex, "creating new user");

                await this.articlesServiceRabbitMqClient.CreateNewUserAsync(new Contracts.UsersApplicationService.CreateNewUserRequest(userName));

                // ждем секунду чтобы создался пользователь
                await Task.Delay(1000);

                var response = await this.articlesServiceRabbitMqClient.GetUserByNameAsync(new Contracts.UsersApplicationService.GetUserByNameRequest(userName));
                user = response.User;
            }

            return user;
        }
    }
}
