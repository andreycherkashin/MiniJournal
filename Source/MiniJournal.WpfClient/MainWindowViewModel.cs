using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;
using Infotecs.MiniJournal.WpfClient.Properties;

namespace Infotecs.MiniJournal.WpfClient
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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

        public MainWindowViewModel()
        {
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

            using (var serviceClient = new ArticlesWebServiceClient())
            {
                User user;

                try
                {
                    var response = await serviceClient.GetUserByNameAsync(new GetUserByNameRequest { UseName = userName });
                    user = response.User;
                }
                catch(Exception)
                {
                    await serviceClient.CreateNewUserAsync(new CreateNewUserRequest { UserName = userName });
                    var response = await serviceClient.GetUserByNameAsync(new GetUserByNameRequest { UseName = userName });
                    user = response.User;
                }
                
                await serviceClient.AddCommentAsync(new AddCommentRequest { ArticleId = articleId, Text = text, UserId = user.Id });
            }

            await this.LoadArticles();
        }

        private async Task AddArticle()
        {
            var userName = this.ArticleUser;
            var text = this.ArticleText;
            byte[] image = this.ArticleImage;

            this.ArticleUser = null;
            this.ArticleText = null;
            this.ArticleImage = null;

            using (var serviceClient = new ArticlesWebServiceClient())
            {
                User user;

                try
                {
                    var response = await serviceClient.GetUserByNameAsync(new GetUserByNameRequest { UseName = userName });
                    user = response.User;
                }
                catch
                {
                    await serviceClient.CreateNewUserAsync(new CreateNewUserRequest { UserName = userName });
                    var response = await serviceClient.GetUserByNameAsync(new GetUserByNameRequest { UseName = userName });
                    user = response.User;
                }                
                
                await serviceClient.CreateArticleAsync(new CreateArticleRequest { Text = text, Image = image, UserId = user.Id });
            }                        

            await this.LoadArticles();
        }

        private void SeedData()
        {
            var user1 = new User
            {
                Id = 1, 
                Name =  "user 1"
            };

            var user2 = new User
            {
                Id = 2,
                Name = "user 2"
            };

            this.Articles = new ObservableCollection<Article>()
            {
                new Article
                {
                    Id = 1,
                    ImageId = "1",
                    Text = "hello world 1",
                    User = user1,
                    Comments = new Comment[]
                    {
                        new Comment { Id = 11, User = user2, Text = "comment 1 1" },
                        new Comment { Id = 12, User = user1, Text = "comment 1 2" },
                        new Comment { Id = 13, User = user2, Text = "comment 1 3" }
                    }
                },
                new Article
                {
                    Id = 2,
                    ImageId = "2",
                    Text = "hello world 2",
                    User = user1,
                    Comments = new Comment[]
                    {
                        new Comment { Id = 21, User = user2, Text = "comment 2 1" },
                        new Comment { Id = 22, User = user1, Text = "comment 2 2" },
                        new Comment { Id = 23, User = user2, Text = "comment 2 3" }
                    }
                },
                new Article
                {
                    Id = 3,
                    ImageId = "3",
                    Text = "hello world 3",
                    User = user2,
                    Comments = new Comment[]
                    {
                        new Comment { Id = 31, User = user1, Text = "comment 3 1" },
                        new Comment { Id = 32, User = user2, Text = "comment 3 2" },
                        new Comment { Id = 33, User = user1, Text = "comment 3 3" }
                    }
                }
            };
        }
    }
}
