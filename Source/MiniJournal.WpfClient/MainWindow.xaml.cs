using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ArticleImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();                   
            if (fileDialog.ShowDialog() == true)
            {
                var image = File.ReadAllBytes(fileDialog.FileName);
                ((MainWindowViewModel)this.DataContext).ArticleImage = image;
            }
        }
    }
}
