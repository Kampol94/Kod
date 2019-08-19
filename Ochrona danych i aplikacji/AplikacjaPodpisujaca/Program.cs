using System;
using BibliotekaKryptograficzna;
using static System.Console;

namespace AplikacjaPodpisujaca
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Wpisz tekst do podpisania: ");
            string dane = ReadLine();
            var podpis = Ochrona.GenerujPodpis(dane);
            WriteLine($"Podpis: {podpis}");
            WriteLine("Klucz publiczny:");
            WriteLine(Ochrona.KluczPubliczny);

            if(Ochrona.KontrolaPodpisu(dane,podpis))
            {
                WriteLine("Poprawne");
            }
            else
            {
                WriteLine("Podpis niepoprawny");
            }

            var falszywyPodpis = podpis.Replace(podpis[1], 'X');
            if (Ochrona.KontrolaPodpisu(dane, falszywyPodpis))
            {
                WriteLine("Poprawne");
            }
            else
            {
                WriteLine("Podpis niepoprawny");
            }
        }
    }
}
