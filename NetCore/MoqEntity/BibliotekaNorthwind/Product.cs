using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaNorthwind
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
