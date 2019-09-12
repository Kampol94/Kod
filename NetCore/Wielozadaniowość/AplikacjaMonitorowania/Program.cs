using System;
using BibliotekaMonitorowania;
using static System.Console;
using System.Linq;

namespace AplikacjaMonitorowania
{
    class Program
    {
        static void Main(string[] args)
        {
            /* WriteLine("Naciśnij ENTER, start stoper");
             ReadLine();
             Nagrywanie.Start();
             int[] duzaTablica = Enumerable.Range(1, 1000).ToArray();
             WriteLine("Naciśnij ENTER, stop stoper");
             ReadLine();
             Nagrywanie.Stop();
             ReadLine();
         */

            int[] numbers = Enumerable.Range(1, 50000).ToArray();
            Nagrywanie.Start();
            WriteLine("string");
            string s = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                s += numbers[i] + ", ";
            }
            Nagrywanie.Stop();
            Nagrywanie.Start();
            WriteLine("StringBuilder");
            var builder = new System.Text.StringBuilder();
            for (int i = 0; i < numbers.Length; i++)
            {
                builder.Append(numbers[i]);
                builder.Append(", ");
            }
            WriteLine(builder); 
            Nagrywanie.Stop();
            ReadLine();
        }
    }
}
