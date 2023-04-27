using System;
using System.Collections.Generic;
using System.Data;
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

namespace Rosi_s_Cook_Book.Pages
{
    /// <summary>
    /// Interaction logic for RecipiesViewPage.xaml
    /// </summary>
    public partial class RecipiesViewPage : Page
    {
        public DataTable ProductsDt { get; set; } = new DataTable();

        public RecipiesViewPage(string Path)
        {
            InitializeComponent();
            PrepareProductsDt();
            LoadRecipie(Path);
        }

        private void PrepareProductsDt() 
        {
            ProductsDt.Columns.Add("Product");
            ProductsDt.Columns.Add("Quantity");
        }
        
        private void LoadRecipie(string xmlFilePath) 
        {
            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);
            
            //Gets the Name
            XmlNodeList NameNodeList = xmlDocument.DocumentElement.GetElementsByTagName("Name");
            string NameNode = NameNodeList.Item(0).InnerText;

            //Gets All the Products and Quantity
            XmlNodeList ProductsNodeList = xmlDocument.DocumentElement.GetElementsByTagName("Products");
            for (int i = 0; i < ProductsNodeList.Item(0).ChildNodes.Count; i++)
            {
                

                DataRow NewDr = ProductsDt.NewRow();
                NewDr[0] = ProductsNodeList.Item(0).ChildNodes.Item(i).ChildNodes.Item(0).InnerText;
                NewDr[1] = ProductsNodeList.Item(0).ChildNodes.Item(i).ChildNodes.Item(1).InnerText;
                ProductsDt.Rows.Add(NewDr);
            }
            
            //Gets the HowItsMade part
            XmlNodeList HowItsMadeNodeList = xmlDocument.DocumentElement.GetElementsByTagName("HowItsMade");
            string HowItsMadeNode = HowItsMadeNodeList.Item(0).InnerText;
            
            //Sets the Textboxes 
            TbName.Text = NameNode;
            TbHowItsMade.Text = HowItsMadeNode;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.OpenRecipiesPage();
        }
    }
}
