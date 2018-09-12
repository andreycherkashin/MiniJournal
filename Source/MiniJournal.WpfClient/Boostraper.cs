using System;
using System.Configuration;
using Autofac;
using Infotecs.MiniJournal.RabbitMqClient;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <summary>
    /// Класс для инициализации приложения.
    /// </summary>
    public static class Boostraper
    {
        private static ILifetimeScope rootScope;
        private static MainWindowViewModel mainViewModel;

        /// <summary>
        /// Модель состояния корневого экрана приложения.
        /// </summary>
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

        /// <summary>
        /// Инициализировать приложение.
        /// </summary>
        public static void Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
            builder.RegisterModule(new RabbitMqClientModule(ConfigurationManager.AppSettings["RabbitMq"]));

            rootScope = builder.Build();
        }

        /// <summary>
        /// Возвращает реализацию запрошенного типа.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <returns>Реализацию запрошенного типа.</returns>
        public static T Resolve<T>()
        {
            return rootScope.Resolve<T>();
        }

        /// <summary>
        /// Очищает ресурсы после остановки приложения.
        /// </summary>
        public static void Stop()
        {
            rootScope.Dispose();
        }
    }
}
