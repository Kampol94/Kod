using DRYLibary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DryLibraryTest
{
    public class EmployeeProcessorTest
    {
        [Theory]
        [InlineData("Timothy","Corey","TimoCore")]
        [InlineData("Tim", "Corey", "TimCore")]
        [InlineData("Tim", "Co", "TimCo")]
        public void GenerateEmployeeId_ShouldCalculate(string firstName, string lastName,string expectedStart)
        {
            //Arange

            //Act
            EmployeeProcessor processor = new EmployeeProcessor();
            string actualStart = processor.GenereteEmployeeId(firstName, lastName).Substring(0, expectedStart.Length);
            //Asert

            Assert.Equal(expectedStart, actualStart);
        }
    }
}
