using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IBorrowableDVD dvd = new DVD
            {
                Borrower = "asd"
            };
            Console.ReadLine();
        }
    }
}
