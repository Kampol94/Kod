using System;
using BibliotekaKryptograficzna;
using static System.Console;

namespace AplikacjaLosujaca
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Wielkosc klucza: ");
            string wielkosc = ReadLine();
            byte[] klucz = Ochrona.PobierzLosowyKluczWektor(int.Parse(wielkosc));
            WriteLine("Klucz:");
            for(int i = 0; i<klucz.Length;i++)
            {
                Write($"{klucz[i]:x2} ");
                if (((i + 1) % 16) == 0) WriteLine();
            }
            WriteLine();
            }
    }
}
