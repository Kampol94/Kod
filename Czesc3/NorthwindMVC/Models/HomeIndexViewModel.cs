using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Packt.CS7
{
    public class HomeIndexViewModel
    {
        public int LiczbaOdwiedzen;
        public IList<Category> Kategorie { get; set; }
        public IList<Product> Produkty { get; set; }
    }
}
