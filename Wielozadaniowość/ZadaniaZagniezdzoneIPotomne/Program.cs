using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace ZadaniaZagniezdzoneIPotomne
{
    class Program
    {
        static void Main(string[] args)
        {
            var zewnetrzne = Task.Factory.StartNew(() =>
            {
                WriteLine("Zewnetrzne zadanie uruchomine..");
                var wewnetrzne = Task.Factory.StartNew(() =>
                {
                    WriteLine("wewnetrzenzadanie uruchomine...");
                    Thread.Sleep(2000);
                    WriteLine("wewnetrze zadanie zakonczone");
                }, TaskCreationOptions.AttachedToParent);
            });
            zewnetrzne.Wait();
            WriteLine("Zewnetrzne zadanie zakonczone");
            ReadLine();
        }
    }
}
