using System;
using BibliotekaKryptograficzna;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Security.Claims;
using static System.Console;

namespace AplikacjaBezpieczna
{
    class Program
    {
        static void Main(string[] args)
        {
            
                Ochrona.Zarejestruj("Alicja", "H45lo", new[] { "Administratorzy" });
                Ochrona.Zarejestruj("Bartek", "H45lo", new[] { "Sprzedaz", "Kierownicy" });
                Ochrona.Zarejestruj("Ewa", "H45lo");

            WriteLine("Login:");
            string nazwaUzytkownika = ReadLine();
            Write("haslo:");
            string haslo = ReadLine();
            Ochrona.Zaloguj(nazwaUzytkownika, haslo);

            if (Thread.CurrentPrincipal == null)
            {
                WriteLine("Nie powiodlo sie");
                return;
            }
            var p = Thread.CurrentPrincipal;
            WriteLine($"IsAuthentical: {p.Identity.IsAuthenticated}");
            WriteLine($"AuthenticalType: {p.Identity.AuthenticationType}");
            WriteLine($"Name: {p.Identity.Name}");
            WriteLine($"IsInRole(\"Administratorzy\"): {p.IsInRole("Administratorzy")}");
            WriteLine($"IsInRole(\"Sprzedaz\"): {p.IsInRole("Sprzedaz")}");
            if (p is ClaimsPrincipal)
            {
                WriteLine($"{p.Identity.Name} ma :");
                foreach( Claim cecha in (p as ClaimsPrincipal).Claims)
                {
                    WriteLine($"{cecha.Type}: {cecha.Value}");
                }
            }

            try
            {
                Ochrona.FunkcjaWymagaUprawniwn(); //administratora


            }
            catch(SystemException ex)
            {
                WriteLine(ex.Message);
            }



        }
    }
}
