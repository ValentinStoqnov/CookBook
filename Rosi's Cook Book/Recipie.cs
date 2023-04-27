using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Rosi_s_Cook_Book
{
    public class Recipie
    {
        private string _name = "";
        private string? _filePath;
        
        
        public string RecipieName { get => _name; set { _name = value; } }
        public string FilePath { get => _filePath; set { _filePath = value; } }




        public Recipie(string name, string filePath)
        {
            RecipieName = name;
            FilePath = filePath;
        }
    }
}
