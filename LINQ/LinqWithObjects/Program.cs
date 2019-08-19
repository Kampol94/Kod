using System;
using System.Linq;
using static System.Console;

namespace LinqWithObjects
{
    class Program
    {

        static void Linq_TablicaCiagowZnakow()
        {
            string[] imiona = new string[] { "Michał", "Piotr", "Jan", "Darek", "Anna", "Krzysztof", "Tadeusz", "Tadeusz", "Celina" };
            //var zapytanie = imiona.Where(new Func<string, bool>(ImionaDluzszeNizCZtery));
            //var zapytanie = imiona.Where(ImionaDluzszeNizCZtery);
            var zapytanie = imiona.Where(imie => imie.Length > 4).OrderBy(imie => imie.Length).ThenBy(imie => imie);

            foreach (string element in zapytanie)
            {
                WriteLine(element);
            }
            
        }
        static void Wyjatki()
        {
            var wyjatki = new Exception[]
            {
             new ArgumentException(),
            new SystemException(),
            new IndexOutOfRangeException(),
            new InvalidOperationException(),
            new NullReferenceException(),
            new InvalidCastException(),
            new OverflowException(),
            new DivideByZeroException(),
            new ApplicationException()

            };

            var wyjatkiLiczbowe = wyjatki.OfType<ArithmeticException>();
            foreach (var wyjatek in wyjatkiLiczbowe)
            {
                WriteLine(wyjatek);
            }
        }

        static bool ImionaDluzszeNizCZtery(string imie)
        {
            return imie.Length > 4;
        }


        static void Main(string[] args)
        {
            Linq_TablicaCiagowZnakow();
            Wyjatki();
        }
    }
}
