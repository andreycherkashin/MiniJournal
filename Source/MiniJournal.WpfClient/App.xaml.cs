using System;
using System.Threading.Tasks;
using System.Windows;
using Serilog;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // set environment variable for serilog configuration
            Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Log.Fatal(args.ExceptionObject as Exception, "DomainUnhandledException");
                MessageBox.Show(args.ExceptionObject.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                Log.Fatal(args.Exception, "TaskSchedulerUnobservedTaskException");
                args.SetObserved();
                MessageBox.Show(args.Exception.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            Current.DispatcherUnhandledException += (o, eventArgs) =>
            {
                Log.Fatal(eventArgs.Exception, "DispatcherUnhandledException");
                eventArgs.Handled = true;
                MessageBox.Show(eventArgs.Exception.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }

        /// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            Boostraper.Start();

            var window = new MainWindow();

            window.Closed += (sender, args) => Boostraper.Stop();
            window.DataContext = Boostraper.RootVisual;

            window.Show();
        }
    }
}
