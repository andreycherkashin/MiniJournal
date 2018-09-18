using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Autofac;
using Infotecs.MiniJournal.Contracts;
using Infotecs.MiniJournal.RabbitMqPublisher;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;

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
            builder.RegisterModule(new RabbitMqModule(rabbitMqConnectionString ?? "amqp://guest:guest@localhost:5672/"));
            return builder.Build();
        }

        /// <summary>
        /// Возвращает пользователя. 
        /// </summary>
        /// <param name="client"><see cref="ArticlesWebServiceClient"/>.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Пользователь.</returns>
        public static User GetUser(this ArticlesWebServiceClient client, string userName)
        {
            User user;
            try
            {
                var response = client.GetUserByNameAsync(new GetUserByNameRequest { UserName = userName }).Result;
                user = response.User;
            }
            catch (FaultException ex) when (ex.Message == "User not found.")
            {
                client.CreateNewUserAsync(new CreateNewUserRequest { UserName = userName }).Wait();

                var response = client.GetUserByNameAsync(new GetUserByNameRequest{ UserName = userName }).Result;
                user = response.User;
            }

            return user;
        }
    }
}
