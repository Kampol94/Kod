using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace PracaZeZbiorami
{
    class Program
    {
        private static void Wyjscie(IEnumerable<string> kohorta, string opis = "")
        {
            if(!string.IsNullOrEmpty(opis))
            {
                WriteLine(opis);
            }
            Write(" ");
            WriteLine(string.Join(", ", kohorta.ToArray()));
        }

        static void Main(string[] args)
        {
            var kohorta1 = new string[]
         { "Rachel", "Gareth", "Jonathan", "George" };
            var kohorta2 = new string[]
                { "Jack", "Stephen", "Daniel", "Jack", "Jared" };
            var kohorta3 = new string[]
              { "Declan", "Jack", "Jack", "Jasmine", "Conor" };
            Wyjscie(kohorta1, "Zespol 1");
            Wyjscie(kohorta2, "Zespol 2");
            Wyjscie(kohorta3, "Zespol 3");
            WriteLine();
            Wyjscie(kohorta2.Distinct(),
              "kohorta2.Distinct(): usuwa duplikaty");
            Wyjscie(kohorta2.Union(kohorta3),
              "kohorta2.Union(kohorta3): laczy zbiory i usuwa duplikaty");
            Wyjscie(kohorta2.Concat(kohorta3),
              "kohorta2.Concat(kohorta3): laczy zbiory i pozostawia duplikaty");
            Wyjscie(kohorta2.Intersect(kohorta3),
              "kohorta2.Intersect(kohorta3): elementy w obu zbiorach");
            Wyjscie(kohorta2.Except(kohorta3),
              "kohorta2.Except(kohorta3): usuwa z pierwszego zbiory elementy ktore znajduja sie takze w drugim");

            Wyjscie(kohorta1.Zip(kohorta2,
              (c1, c2) => $"{c1} przeciw {c2}"),
              "kohorta1.Zip(kohorta2, (c1, c2) => $\"{c1} przeciw {c2}\")" +
              ": dopasowuje elementy na podstawie ich pozycji w sekwencji");
        }
    }
}
