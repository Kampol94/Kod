using System;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace PracaZEFCore
{
    class Program

    {
        static void ZapytanieOKategorie()
        {
            using (var db = new Northwind())
            {
                var fabrykaProtokolu = db.GetService<ILoggerFactory>();
                fabrykaProtokolu.AddProvider(new DostawcaProtokoluKonsli());
                WriteLine("Lista kategori i liczba przypisanych nim produktów ");
                IQueryable<Category> kategorie; //= db.Categories.Include(c => c.Products);
                WriteLine("Ładowanie chetne (T/N)");
                bool ladowanieChetne = (ReadKey().Key == ConsoleKey.T);
                bool ladowanieJawne = false;

                WriteLine();
                if(ladowanieChetne)
                {
                    kategorie = db.Categories.Include(c => c.Products);
                }
                else
                {
                    kategorie = db.Categories;
                    WriteLine("Ładowanie Jawne? (T/N)");
                    ladowanieJawne = (ReadKey().Key == ConsoleKey.T);
                    WriteLine();
                }

                foreach (Category k in kategorie)
                {
                    if (ladowanieJawne)
                    {
                        Write($"Jawnie załadować produkty z kategori {k.CategoryName}? (T/N):");
                        if (ReadKey().Key == ConsoleKey.T)
                        {
                            var produkty = db.Entry(k).Collection(k2 => k2.Products);
                            if (!produkty.IsLoaded) produkty.Load();
                        }
                        WriteLine();
                    }
                    WriteLine($"Kategoria {k.CategoryName} ma {k.Products.Count} produktów");
                }
            }
        }

        static void ZapytanieOProdukty()
        {
            using (var db = new Northwind())
            {
                var fabrykaProtokolu = db.GetService<ILoggerFactory>();
                fabrykaProtokolu.AddProvider(new DostawcaProtokoluKonsli());
                string wejscie;
                decimal cena;
                do
                {
                    Write("Podaj cenę <");
                    wejscie = ReadLine();

                } while (!decimal.TryParse(wejscie, out cena));

                IQueryable<Product> produkty = db.Products
                    .Where(produkt => produkt.Cost > cena)
                    .OrderByDescending(produkt => produkt.Cost);
                foreach(Product produkt in produkty)
                {
                    WriteLine($"{produkt.ProductID}: {produkt.ProductName} {produkt.Cost}. Wmagazynie jest {produkt.Stock}");
                }

            }
        

        }

        static void ZapytanieZLike()
        {
            using (var db = new Northwind())
            {
                var fabrykaProtokolu = db.GetService<ILoggerFactory>();
                fabrykaProtokolu.AddProvider(new DostawcaProtokoluKonsli());

                Write("Czesc nazwy produktu");
                string input = ReadLine();

                IQueryable<Product> produkty = db.Products.Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

                foreach(Product product in produkty)
                {
                    WriteLine($" {product.ProductName} jest {product.Stock}  wytwarzany {product.Discontinued}");
                }
            }
        }

        static bool DodajProdukt(int idKategorii, string nazwaProduktu, decimal? cena)
        {
            using (var db = new Northwind())
            {
                var nowyProdukt = new Product
                {
                    CategoryID = idKategorii,
                    ProductName = nazwaProduktu,
                    Cost = cena

                };

                db.Products.Add(nowyProdukt);

                int zmienione = db.SaveChanges();
                return (zmienione == 1);
            }

        }

        static void WypiszProdukty()
        {
            using (var db = new Northwind())
            {
                foreach (var pozycja in db.Products.OrderByDescending(p => p.Cost))
                {
                    WriteLine($"|  {pozycja.ProductID:000}  |  {pozycja.ProductName,-35}  |  {pozycja.Cost,8:$#,##0.00} |  {pozycja.Stock,5}  |  {pozycja.Discontinued}  |");
                }
            }
        }

        static bool ZwiekszenieCeny(string nazwa, decimal kwota)
        {
            using (var db = new Northwind())
            {
                Product produktDoAktualizacji = db.Products.First(p => p.ProductName.StartsWith(nazwa));
                produktDoAktualizacji.Cost += kwota;
                int zmienione = db.SaveChanges();
                return (zmienione == 1);
            }
        }

        static int UsunProdukty(string nazwa)
        {
            using (var db = new Northwind())
            {
                using (IDbContextTransaction t = db.Database.BeginTransaction())
                {
                    WriteLine($"Transakcja została uruchomiano z poziomu izolacji: {t.GetDbTransaction().IsolationLevel}");


                    IEnumerable<Product> produkty = db.Products.Where(p => p.ProductName.StartsWith(nazwa));
                    db.Products.RemoveRange(produkty);
                    int zmienione = db.SaveChanges();
                    t.Commit();
                    return zmienione;
                }
            }
        }
        static void Main(string[] args)
        {
            //ZapytanieOKategorie();

            //ZapytanieOProdukty();
            //ZapytanieZLike();

            //DodajProdukt(6, "Burgery", 500M);
            //ZwiekszenieCeny("Burger", 20M);
            int usuniete = UsunProdukty("Burg");
            WriteLine($"{usuniete} produktów zostało usuniete");
            WypiszProdukty();
        }
    }
}
