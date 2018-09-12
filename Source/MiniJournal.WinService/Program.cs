using System;
using Autofac;
using Serilog;
using Topshelf;

namespace Infotecs.MiniJournal.WinService
{
    /// <summary>
    /// Class for EntryPoint method.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry Point.
        /// </summary>
        /// <param name="args">Args array.</param>
        public static void Main(string[] args)
        {
            // set environment variable for serilog configuration
            Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);

            // configure serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            // build container
            var builder = new ContainerBuilder();
            builder.RegisterModule<WinServiceModule>();
            IContainer container = builder.Build();

            // build service
            Host host = HostFactory.New(x =>
            {
                x.Service(s => container.Resolve<WindowsService>());

                x.RunAsLocalSystem();
                x.StartManually();

                x.DependsOn("RabbitMQ");
                x.DependsOn("postgresql-x64-10");

                x.SetDescription("Mini Journal Windows Service");
                x.SetDisplayName("Mini Journal");
                x.SetServiceName("mini-journal");

                x.UseSerilog();
            });

            TopshelfExitCode returnCode = host.Run();

            var exitCode = (int)Convert.ChangeType(returnCode, returnCode.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
