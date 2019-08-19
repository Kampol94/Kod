using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKryptograficzna
{
    public class Uzytkownik
    {
        public string Nazwisko { get; set; }
        public string Sol { get; set; }
        public string SkrolSolonegoHasla { get; set; }
        public string[] Role { get; set; }

    }
}
