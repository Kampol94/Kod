using AutoFixture;
using BibliotekaNorthwind;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using BibliotekaNorthwind;
using ConsoleApp4;

namespace XUnitTestProject1
{
    public class OperacjaNaZbiorzeTest
    {
        [Fact]
        public void Test1()
        {
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>().With(c => c.City, "London").Create();
            var customers = new List<Customer>
                  {
                    customer,
                    fixture.Build<Customer>().With(u => u.City, "Berlin").Create(),
                    fixture.Build<Customer>().With(u => u.City, "Warszawa").Create()
                  }.AsQueryable();

            var customerMock = CreateDbSetMock(customers);

            var northwindContextMock = new Mock<Nothwind>();
            northwindContextMock.Setup(x => x.Customers).Returns(customerMock.Object);

            var operacjenazbiorze = new OperacjeNaZbiorze(northwindContextMock.Object);

            // Act
            var listOfCities = operacjenazbiorze.ListOfCity();

            // Assert
            Assert.Equal(new List<string> { "London","Berlin","Warszawa" }, listOfCities);



        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }

}
