using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BibliotekaNorthwind;

namespace ConsoleApp4
{
    public class OperacjeNaZbiorze
    {
        private readonly Nothwind _nothwind;
        public OperacjeNaZbiorze(Nothwind nothwind)
        {
            _nothwind = nothwind;
        }

        public List<string> ListOfClientsInCity(string city)
        {
            

                var output = _nothwind.Customers.Where(c => c.City == city).Select(c =>c.CompanyName ).ToList();
                return output;
            
            
        }
        public List<string> ListOfCity()
        {
            
                
                var output = _nothwind.Customers.Select(c => c.City).Distinct().ToList();
                return output;
            
        }

        public static void WriteCollection(List<string> collection)
        {
            foreach (string item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}
