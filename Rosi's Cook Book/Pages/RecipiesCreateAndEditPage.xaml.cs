using System;
using System.Collections.Generic;
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

namespace Rosi_s_Cook_Book.Pages
{
    /// <summary>
    /// Interaction logic for RecipiesCreateAndEditPage.xaml
    /// </summary>
    public partial class RecipiesCreateAndEditPage : Page
    {
        public DataTable ProductsDt { get; set; } = new DataTable();
        private string createOrEdit = "";
        private string recipieFilePath = "";
        private bool recipieGotCreated;
        public RecipiesCreateAndEditPage(string CreateOrEdit)
        {
            createOrEdit = CreateOrEdit;

            InitializeComponent();

            PrepareDataTable();
        }

        public RecipiesCreateAndEditPage(string CreateOrEdit, string FilePath) : this(CreateOrEdit)
        {
            TxtBoxName.IsEnabled = false;
            if (CreateOrEdit == "Edit")
            {
                recipieFilePath = FilePath;
                LoadRecipie(FilePath);
            }
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
            TxtBoxName.Text = NameNode;
            TxtBoxHowItsMade.Text = HowItsMadeNode;
        }

        private void CreateRecepie() 
        {
            string path = @$"{AppDomain.CurrentDomain.BaseDirectory}Data\{TxtBoxName.Text}.xml";
            if (File.Exists(path))
            {
                MessageBox.Show("Вече съществува рецепта с това име!");
                recipieGotCreated = false;
            }
            else 
            {
                CreateXmlFile(path);
                recipieGotCreated = true;
            } 
        }

        private void EditRecepie(string xmlFilePath)
        {
            CreateXmlFile(xmlFilePath);
        }

        private void CreateXmlFile(string Path) 
        {
            using (XmlWriter writer = XmlWriter.Create(Path))
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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            switch (createOrEdit) 
            {
                case "Create":
                    CreateRecepie();
                    if (recipieGotCreated) 
                    {
                        NavigationHelper.OpenRecipiesPage();
                    }
                    break;
                case "Edit":
                    EditRecepie(recipieFilePath);
                    NavigationHelper.OpenRecipiesPage();
                    break;
            }  
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.OpenRecipiesPage();
        }
    }
}
