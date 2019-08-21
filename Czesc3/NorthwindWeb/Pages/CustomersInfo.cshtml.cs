using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.CS7;
using Microsoft.EntityFrameworkCore;

namespace NorthwindWeb.Pages
{
    public class CustomersInfoModel : PageModel
    {
        private Northwind db;

        public CustomersInfoModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public IEnumerable<string> CustomersInfo { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Klient";
            //CustomersInfo = db.Customers.Where(p => EF.Functions.Like(p.ContactName, $"%{customer1}%");
        }

    }
}
