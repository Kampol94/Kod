using System;
using BibliotekaKryptograficzna;
using static System.Console;

namespace AplikacjaSkrotow
{
    class Program
    {
        static void Main(string[] args)
        {
            var alicja = Ochrona.Zarejestruj("Alicja", "H45lo");
            WriteLine($"{alicja.Nazwisko}");
            WriteLine($"{alicja.Sol}");
            WriteLine(alicja.SkrolSolonegoHasla);
            WriteLine();

            bool hasloPrawidlowe = false;
            while(!hasloPrawidlowe)
            {
                Write("Login:");
                string nazwa = ReadLine();
                Write("haslo");
                string haslo = ReadLine();
                hasloPrawidlowe = Ochrona.SprawdzHaslo(nazwa, haslo);
                if (hasloPrawidlowe)
                {
                    WriteLine("Ok");
                }
                else
                {
                    WriteLine("Not OK");
                }
            }
        

        }
    }
}
