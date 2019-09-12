using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace PracaZZadaniami
{
    class Program
    {
        static void MethodA()
        {
            WriteLine("Starting Method A...");
            Thread.Sleep(3000);
            WriteLine("Finished Method A.");
        }

        static void MethodB()
        {
            WriteLine("Starting Method B...");
            Thread.Sleep(2000);
            WriteLine("Finished Method B.");
        }

        static void MethodC()
        {
            WriteLine("Starting Method C...");
            Thread.Sleep(1000);
            WriteLine("Finished Method C.");
        }

        static decimal WywolajSerwisWWW()
        {
            WriteLine("Uruchamiam wywołanie serwisu WWW...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            WriteLine("Zakończenie wywołania serwisu.");
            return 89.99M;
        }

        static string WywolajProcedureOsadzania(decimal kwota)
        {
            WriteLine("Uruchamiam wywołanie procedury osadzania...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            WriteLine("Zakończone wywoływanie osadzania.");
            return $"12 produktów kosztuje więcej niż {kwota:C}.";
        }
        static void Main(string[] args)
        {
            
            var timer = Stopwatch.StartNew();
            /* WriteLine("Uruchamianie meton synchronicznie w jednym wątku");
             MethodA();
             MethodB();
             MethodC();
            WriteLine($"Upłyneło {timer.ElapsedMilliseconds:#,##0}ms.");
            
            WriteLine("Uruchamianie meton asynchronicznie w wielu wątkach");
            Task zadanieA = new Task(MethodA);
            zadanieA.Start();
            Task zadanieB = Task.Factory.StartNew(MethodB);
            Task zadanieC = Task.Run(new Action(MethodC));
            Task[] zadania = { zadanieA, zadanieB, zadanieC };
            Task.WaitAll(zadania);
            WriteLine($"Upłyneło {timer.ElapsedMilliseconds:#,##0}ms.");
            ReadLine();
            */
            WriteLine("Przekazywanie wyniku jednej metody na wejscie kolejnej.");

            var zadanie =
              Task.Factory.StartNew(WywolajSerwisWWW)
              .ContinueWith(previousTask =>
                WywolajProcedureOsadzania(previousTask.Result));

            WriteLine($"{zadanie.Result}");
            WriteLine($"Upłyneło {timer.ElapsedMilliseconds:#,##0}ms.");

        }
    }
}
