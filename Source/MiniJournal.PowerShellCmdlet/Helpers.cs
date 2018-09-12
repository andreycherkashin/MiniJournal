using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infotecs.MiniJournal.RabbitMqClient;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;
using CreateNewUserRequest = Infotecs.MiniJournal.Contracts.UsersApplicationService.CreateNewUserRequest;
using GetUserByNameRequest = Infotecs.MiniJournal.Contracts.UsersApplicationService.GetUserByNameRequest;
using User = Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities.User;

namespace MiniJournal.PowerShellCmdlet
{
    public static class Helpers
    {
        public static ArticlesWebServiceClient CreateWcfServiceClient(string serviceUrl)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri(serviceUrl ?? "http://localhost:61060/ArticlesWebService.svc"));
            return new ArticlesWebServiceClient(binding, endpoint);
        }

        public static IContainer CreateContainer(string rabbitMqConnectionString)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RabbitMqClientModule(rabbitMqConnectionString ?? "guest:guest@localhost:5672/"));
            return builder.Build();
        }

        public static User GetUser(this IArticlesServiceRabbitMqClient client, string userName)
        {
            User user;
            try
            {
                var response = client.GetUserByNameAsync(new GetUserByNameRequest(userName)).Result;
                user = response.User;
            }
            catch (RawRabbit.Exceptions.MessageHandlerException ex) when (ex.InnerMessage == "User not found.")
            {
                client.CreateNewUserAsync(new CreateNewUserRequest(userName)).Wait();

                // ждем секунду чтобы создался пользователь
                Task.Delay(1000).Wait();

                var response = client.GetUserByNameAsync(new GetUserByNameRequest(userName)).Result;
                user = response.User;
            }

            return user;
        }
    }
}
