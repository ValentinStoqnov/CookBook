using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rosi_s_Cook_Book
{
    public class RecipieFullInfo
    {
        private string _name = "";
        private DataTable? _products;
        private string? _cookingMethods;
        private string? _filePath;


        public string RecipieName { get => _name; set { _name = value; } }
        public string CookingMethods { get => _cookingMethods; set { _cookingMethods = value; } }
        public DataTable? Products { get => _products; set { _products = value; } }
        public string FilePath { get => _filePath; set { _filePath = value; } }

        public RecipieFullInfo(string name, DataTable products, string methods, string filePath)
        {
            RecipieName = name;
            Products = products;
            CookingMethods = methods;
            FilePath = filePath;
        }
    }
}
