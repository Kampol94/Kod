using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRYLibary;


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Imię");
            string firstName = Console.ReadLine();

            Console.WriteLine("Nazwisko");
            string lastName = Console.ReadLine();

            EmployeeProcessor processor = new EmployeeProcessor();
            string employeeId = processor.GenereteEmployeeId(firstName, lastName);

            Console.WriteLine($"Twój id jest: {employeeId}");

            Console.ReadLine();
        }
    }
}
