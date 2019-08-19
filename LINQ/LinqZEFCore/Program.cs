using System;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;
using System.IO;


namespace LinqZEFCore
{
    class Program
    {
        static void Main(string[] args)
        {


            using (var db = new Northwind())
            {
                var zapytanie = db.Products
                    .PrzetwarzajSekwencje()
                    .Where(product => product.UnitPrice < 10M)
                        .OrderByDescending(product => product.UnitPrice)
                        .Select(product => new
                        {
                            product.ProductID,
                            product.ProductName,
                            product.UnitPrice
                        });
                WriteLine("Produkty kosztujace mniej niz 10zł");
                foreach (var item in zapytanie)
                {
                    WriteLine($"{item.ProductID}: {item.ProductName} kosztuje {item.UnitPrice:zł#,##0.00}");
                }
                WriteLine();
            };



            using (var db = new Northwind())
            {
                var kataegorie = db.Categories.Select(
                    k => new { k.CategoryID, k.CategoryName }).ToArray();

                var produkty = db.Products.Select(
                    p => new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.CategoryID
                    }).ToArray();

                var zapytanieJoin = kataegorie.Join(produkty,
                    kataegoria => kataegoria.CategoryID,
                    product => product.CategoryID,
                    (c, p) => new
                    {
                        c.CategoryName,
                        p.ProductName,
                        p.ProductID
                    }).OrderBy(p => p.ProductID);
                foreach (var pozycja in zapytanieJoin)
                {
                    WriteLine($"{pozycja.ProductID}: {pozycja.ProductName} w kategorii {pozycja.CategoryName}.");
                }

            }

            using (var db = new Northwind())
            {
                var kataegorie = db.Categories.Select(
                    k => new { k.CategoryID, k.CategoryName }).ToArray();

                var produkty = db.Products.Select(
                    p => new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.CategoryID
                    }).ToArray();

                var zapytanieGroupJoin = kataegorie.GroupJoin(produkty,
                    kategoria => kategoria.CategoryID,
                    produkt => produkt.CategoryID,
                    (k, Products) => new
                    {
                        k.CategoryName,
                        Products = Products.OrderBy(p => p.ProductName)
                    }
                    );
                foreach (var element in zapytanieGroupJoin)
                {
                    WriteLine($"{element.CategoryName} ma {element.Products.Count()} produktow.");
                    foreach (var produkt in element.Products)
                    {
                        WriteLine($"  {produkt.ProductName}");
                    }
                }


            }


            using (var db = new Northwind())
            {
               

                WriteLine("Własne metody Linq:");
                WriteLine($" Średnia liczba jednostek w magazynie: {db.Products.Average(p => p.UnitsInStock):N0}");
                WriteLine($"  Średniacena : {db.Products.Average(p => p.UnitPrice):$#,##0.00}");
                WriteLine($"  Mediana jednostek: {db.Products.Median(p => p.UnitsInStock)}");
                WriteLine($"  Mediana ceny: {db.Products.Median(p => p.UnitPrice):$#,##0.00}");
                WriteLine($"  Dominanta jednostek: {db.Products.Mode(p => p.UnitsInStock)}");
                WriteLine($"  Dominanta ceny: {db.Products.Mode(p => p.UnitPrice):$#,##0.00}");

                var produktyForXml = db.Products.ToArray();
                var xml = new XElement("produkty",
                  from p in produktyForXml
                  select new XElement("produkt",
                    new XAttribute("id", p.ProductID),
                    new XAttribute("cena", p.UnitPrice),
                    new XElement("nazwa", p.ProductName)));
                WriteLine(xml.ToString());


                XDocument doc = XDocument.Load("ustawienia.xml");
                var ustawieniaAplikacji = doc.Descendants(
                  "appSettings").Descendants("add")
                  .Select(node => new
                  {
                      Key = node.Attribute("key").Value,
                      Value = node.Attribute("value").Value
                  })
                 .ToArray();
                foreach (var item in ustawieniaAplikacji)
                {
                    WriteLine($"{item.Key}: {item.Value}");
                }
            }

            


        }
    }
}
