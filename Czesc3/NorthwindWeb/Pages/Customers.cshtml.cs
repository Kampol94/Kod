using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.CS7;

namespace NorthwindWeb.Pages
{
    public class CustomersModel : PageModel
    {
        private Northwind db;

        public CustomersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public IEnumerable<string> Customers { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Klienci";
            Customers = db.Customers.Select(s => s.CompanyName).ToArray();
        }


    }
}