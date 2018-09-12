using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Autofac;
using Infotecs.MiniJournal.RabbitMqClient;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;
using RawRabbit.Exceptions;
using CreateNewUserRequest = Infotecs.MiniJournal.Contracts.UsersApplicationService.CreateNewUserRequest;
using GetUserByNameRequest = Infotecs.MiniJournal.Contracts.UsersApplicationService.GetUserByNameRequest;
using GetUserByNameResponse = Infotecs.MiniJournal.Contracts.UsersApplicationService.GetUserByNameResponse;
using User = Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities.User;

namespace MiniJournal.PowerShellCmdlet
{
    /// <summary>
    /// Класс содержит общие методы, используемые во всех командлетах.
    /// </summary>
    public static class Helpers
    {        
        /// <summary>
        /// Создает прокси-клиента веб службы.
        /// </summary>
        /// <param name="serviceUrl">Адрес веб службы.</param>
        /// <returns>Прокси-клиент веб службы.</returns>
        public static ArticlesWebServiceClient CreateWcfServiceClient(string serviceUrl)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri(serviceUrl ?? "http://localhost:61060/ArticlesWebService.svc"));
            return new ArticlesWebServiceClient(binding, endpoint);
        }

        /// <summary>
        /// Создает IoC контейнер.
        /// </summary>
        /// <param name="rabbitMqConnectionString">Строка подключения к брокеру сообщений.</param>
        /// <returns>IoC контейнер.</returns>
        public static IContainer CreateContainer(string rabbitMqConnectionString)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RabbitMqClientModule(rabbitMqConnectionString ?? "guest:guest@localhost:5672/"));
            return builder.Build();
        }

        /// <summary>
        /// Возвращает пользователя. 
        /// </summary>
        /// <param name="client"><see cref="IArticlesServiceRabbitMqClient"/>.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Пользователь.</returns>
        public static User GetUser(this IArticlesServiceRabbitMqClient client, string userName)
        {
            User user;
            try
            {
                GetUserByNameResponse response = client.GetUserByNameAsync(new GetUserByNameRequest(userName)).Result;
                user = response.User;
            }
            catch (MessageHandlerException ex) when (ex.InnerMessage == "User not found.")
            {
                client.CreateNewUserAsync(new CreateNewUserRequest(userName)).Wait();

                // ждем секунду чтобы создался пользователь
                Task.Delay(1000).Wait();

                GetUserByNameResponse response = client.GetUserByNameAsync(new GetUserByNameRequest(userName)).Result;
                user = response.User;
            }

            return user;
        }
    }
}
