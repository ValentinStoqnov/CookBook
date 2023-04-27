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
    /// Interaction logic for RecipiesCreateAndEditPage.xaml
    /// </summary>
    public partial class RecipiesCreateAndEditPage : Page
    {
        public DataTable ProductsDt { get; set; } = new DataTable();

        
        public RecipiesCreateAndEditPage(string CreateOrEdit)
        {
            InitializeComponent();

            PrepareDataTable();
        }

        private void CreateRecepie() 
        {
            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}Data\{TxtBoxName.Text}.xml";

            using (XmlWriter writer = XmlWriter.Create(path))
            {


                writer.WriteStartDocument();
                writer.WriteStartElement("Recepie");
                writer.WriteElementString("Name", $"{TxtBoxName.Text}");
                writer.WriteStartElement("Products");
                for (int i = 0; i < ProductsDt.Rows.Count; i++)
                {
                    writer.WriteStartElement("Product" + i);
                    writer.WriteElementString("Name", $"{ProductsDt.Rows[i][0]}");
                    writer.WriteElementString("Quantity", $"{ProductsDt.Rows[i][1]}");
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteElementString("HowItsMade", $"{TxtBoxHowItsMade.Text}");
                writer.WriteEndElement();
                writer.Flush();
            }

        }

        private void PrepareDataTable() 
        {
           DataColumn ProduktCol = ProductsDt.Columns.Add("Products", typeof(string));
           DataColumn KolichestvoCol = ProductsDt.Columns.Add("Quantity", typeof(string));
        }

        private void EditRecepie() 
        { 
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            CreateRecepie();
            NavigationHelper.OpenRecipiesPage();  
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.OpenRecipiesPage();
        }
    }
}
