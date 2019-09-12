using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindMVC.Models;
using Packt.CS7;
using Microsoft.EntityFrameworkCore;

namespace NorthwindMVC.Controllers
{
    public class HomeController : Controller
    {
        public Northwind bd;
        public HomeController(Northwind wstrzyknientyKontekst)
        {
            bd = wstrzyknientyKontekst;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                LiczbaOdwiedzen = (new Random()).Next(1, 1001),
                Kategorie = bd.Categories.ToList(),
                Produkty = bd.Products.ToList()
            };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DaneProduktu(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Musisz podac w scieżce ID produktu np. /Home/DaneProduktu/21");
            }
            var model = bd.Products.SingleOrDefault(p => p.ProductID == id);
            if (model == null)
            {
                return NotFound($"A product with the ID of {id} was not found.");
            }
            return View(model); // pass model to view 
        }

        public IActionResult ProduktyDrozszeNiz(decimal? cena)
        {
            if (!cena.HasValue)
            {
                return NotFound("Podaj cene w zapytaniu np. /Home/ProduktyDrozszeNiz?cena=50");
            }
            var model = bd.Products.Include(p => p.Category).Include(
              p => p.Supplier).Where(p => p.UnitPrice > cena).ToArray();
            if (model.Count() == 0)
            {
                return NotFound($"Zaden produkt nie kosztuje wiecej niz {cena:C}.");
            }
            ViewData["MksymalnaCena"] = cena.Value.ToString("C");
            return View(model); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
