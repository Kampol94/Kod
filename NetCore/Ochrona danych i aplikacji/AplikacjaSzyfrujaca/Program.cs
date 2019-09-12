using System;
using BibliotekaKryptograficzna;
using static System.Console;

namespace AplikacjaSzyfrujaca
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Wpisz tekst do szyfrowania:  ");
            string tekst = ReadLine();
            Write("Wpisz haslo :");
            string haslo = ReadLine();
            string tekstZaszyfrowany = Ochrona.Szyfruj(tekst, haslo);
            WriteLine(tekstZaszyfrowany);
            Write("Podaj haslo");
            string haslo2 = ReadLine();
            try
            {
                string jawnyTekst = Ochrona.Odszyfruj(tekstZaszyfrowany, haslo2);
                WriteLine(jawnyTekst);
            }
            catch
            {
                WriteLine("Zle haslo");
            }
        }
    }
}
