using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rosi_s_Cook_Book
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            OpenRecipiesPage();
            CheckForDataFolder();
        }

        private void OpenRecipiesPage() 
        {
            RecipiesPage RcpPage = new RecipiesPage();
            ContentFrame.Content = RcpPage;
        }

        private void CheckForDataFolder() 
        { 
            string DataFolderPath = $"{AppDomain.CurrentDomain.BaseDirectory}Data";
            Directory.CreateDirectory(DataFolderPath);
        }
    }
}
