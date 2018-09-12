using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ArticleImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                byte[] image = File.ReadAllBytes(fileDialog.FileName);
                ((MainWindowViewModel)this.DataContext).ArticleImage = image;
            }
        }
    }
}
