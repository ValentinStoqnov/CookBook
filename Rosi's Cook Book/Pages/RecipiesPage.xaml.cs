using Rosi_s_Cook_Book.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Xml;

namespace Rosi_s_Cook_Book
{
    /// <summary>
    /// Interaction logic for RecipiesPage.xaml
    /// </summary>
    public partial class RecipiesPage : Page
    {
        public ObservableCollection<Recipie> RecipieCollection { get; set; } = new ObservableCollection<Recipie>();


        public RecipiesPage()
        {
            InitializeComponent();

            GetAllRecipies();
            SetUpSearchBar();
            
        }

        private void SetUpSearchBar() 
        {
            RecipiesLv.ItemsSource = RecipieCollection;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(RecipiesLv.ItemsSource);
            view.Filter = RecipiesFilter;
        }

        private bool RecipiesFilter(object item)
        {
            if (SearchBar.Text == String.Empty)
            {
                return true;
            }
            else 
            {
                return ((item as Recipie).RecipieName.IndexOf(SearchBar.Text, StringComparison.OrdinalIgnoreCase) >= 1);
            }      
        }

        private void GetAllRecipies() 
        {
            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}Data";
            foreach (string xmlFilePath in Directory.GetFiles(path)) 
            { 
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlFilePath);
                XmlNodeList NameNodeList = xmlDocument.DocumentElement.GetElementsByTagName("Name");
                string NameNode = NameNodeList.Item(0).InnerText;



                Recipie rcp = new Recipie(NameNode, xmlFilePath);
                RecipieCollection.Add(rcp);
            }
        }
        
        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.OpenRecipiesCreatePage();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Recipie rcp = RecipiesLv.SelectedItem as Recipie;
            NavigationHelper.OpenRecipiesEditPage(rcp.FilePath);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Recipie rcp = RecipiesLv.Items.CurrentItem as Recipie;
            
            MessageBoxResult messageBoxResult = MessageBox.Show("Сигурни ли сте че искате да изтриете рецепта: " + rcp.RecipieName + "?", "Изтриване", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes) 
            {
                RecipieCollection.Remove(rcp);
                File.Delete(rcp.FilePath);
            } 
        }

        private void RecipiesLv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Recipie rcp = RecipiesLv.SelectedItem as Recipie;
            NavigationHelper.OpenRecipesViewPage(rcp.FilePath);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RecipiesLv.ItemsSource).Refresh();
        }
    }
}
