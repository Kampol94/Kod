using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;


namespace SychronizacjaDostepuDoZasobow
{
    class Program
    {
        static Random r = new Random();
        static string Komunikat;

        static object koncha = new object();

        static void MethodA()
        {
            lock (koncha)
            {

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Komunikat += "A";
                    Write(".");
                }
            }

        }

        static void MethodB()
        {
            try
            {
                Monitor.TryEnter(koncha, TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Komunikat += "B";
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(koncha);
            }

        }
        static void Main(string[] args)
        {

            Stopwatch stoper = Stopwatch.StartNew();

            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);

            Task.WaitAll(new Task[] { a, b });
            WriteLine();
            WriteLine($"Results: {Komunikat}.");
            WriteLine($"{stoper.ElapsedMilliseconds:#,##0} ms.");
        }
    }
}
