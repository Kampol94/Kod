using System;
using System.Diagnostics;
using static System.Diagnostics.Process;
using static System.Console;

namespace BibliotekaMonitorowania
{
    public static class Nagrywanie
    {
        static Stopwatch timer = new Stopwatch();
        static long fizyczneBajtyPrzed = 0;
        static long wirtualneBajtyPrzed = 0;

        public static void Start()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            fizyczneBajtyPrzed = GetCurrentProcess().WorkingSet64;
            wirtualneBajtyPrzed =
              GetCurrentProcess().VirtualMemorySize64;
            timer.Restart();
        }

        public static void Stop()
        {
            timer.Stop();
            long fizyczneBajtyPo = GetCurrentProcess().WorkingSet64;
            long wirtualneBajtyPo =
              GetCurrentProcess().VirtualMemorySize64;
            WriteLine("Zakończono nagrywanie.");
            WriteLine($" Wykorzystano {fizyczneBajtyPo - fizyczneBajtyPrzed:N0} fizycznych bajtów.");
            WriteLine($"Wykorzystanp {wirtualneBajtyPo - wirtualneBajtyPrzed:N0} wirtualnych bajtów.");
            WriteLine($"Uplyneło {timer.Elapsed} czasu.");
            WriteLine($"{timer.ElapsedMilliseconds:N0} milisekund.");
        }


    }
}
