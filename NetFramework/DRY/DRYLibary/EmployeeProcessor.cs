using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRYLibary
{
    public class EmployeeProcessor
    {
        public string GenereteEmployeeId(string firstName, string lastName)
        {
            string output = $@"{ GetPartOfName(firstName,4) }{ GetPartOfName(lastName, 4) }{ DateTime.Now.Millisecond }";
            return output;
        }

        private string GetPartOfName(string name, int numberOfCharacters)
        {
            string output = name;

            if (name.Length > numberOfCharacters)
            {
                output= name.Substring(0, numberOfCharacters);
            }
            return output;
        }
    }
}
