using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Infotecs.MiniJournal.WpfClient
{
    public static class Boostraper
    {
        private static ILifetimeScope rootScope;
        private static MainWindowViewModel mainViewModel;

        public static object RootVisual
        {
            get
            {
                if (rootScope == null)
                {
                    Start();
                }

                mainViewModel = rootScope.Resolve<MainWindowViewModel>();
                return mainViewModel;
            }
        }

        public static void Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
            builder.RegisterModule(new RabbitMqClient.RabbitMqClientModule(ConfigurationManager.AppSettings["RabbitMq"]));
            
            rootScope = builder.Build();
        }

        public static T Resolve<T>()
        {
            return rootScope.Resolve<T>();
        }

        public static void Stop()
        {
            rootScope.Dispose();
        }
    }
}
