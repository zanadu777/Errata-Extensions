using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Windows;
using Errata.IO;

namespace IsolatedStorageDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);


            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\IsolatedStorage");
        }

        private void btWriteAllText_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            var path = txtPath.Text;
            var text = txtSource.Text;
            isoStore.WriteAllText(path, text);
        }

        private void btReadAll_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            var path = txtPath.Text;
            var text = isoStore.ReadAllText(path);
            txtFileText.Text = text;

        }
    }
}
