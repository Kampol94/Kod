using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Security.Principal;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Security.Claims;
using static System.Console;

namespace BibliotekaKryptograficzna
{
    public static class Ochrona
    {
        private static readonly byte[] sol = Encoding.Unicode.GetBytes("7BANANOW");
        private static readonly int iteracje = 2000;

        public static string Szyfruj(string jawnyTekst, string haslo)
        {
            byte[] jawneBajty = Encoding.Unicode.GetBytes(jawnyTekst);

            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(haslo, sol, iteracje);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(jawneBajty, 0, jawneBajty.Length);
            }
            return Convert.ToBase64String(ms.ToArray());

        }
        public static string Odszyfruj(string zaszyfrowanyTekst, string haslo)
        {
            byte[] zaszyfrowaneBajty = Convert.FromBase64String(zaszyfrowanyTekst);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(haslo, sol, iteracje);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(zaszyfrowaneBajty, 0, zaszyfrowaneBajty.Length);
            }
            return Encoding.Unicode.GetString(ms.ToArray());
        }

        private static Dictionary<string, Uzytkownik> Uzytkownicy = new Dictionary<string, Uzytkownik>();

        public static Uzytkownik Zarejestruj(string nazwaUzytkownika, string haslo, string[] role = null)
        {
            var rng = RandomNumberGenerator.Create();
            var bajtySoli = new byte[16];
            rng.GetBytes(bajtySoli);
            var tekstSoli = Convert.ToBase64String(bajtySoli);

            var sha = SHA256.Create();
            var soloneHaslo = haslo + tekstSoli;
            var skrotSolonegoHasla = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(soloneHaslo)));
            var uzytkownik = new Uzytkownik
            {
                Nazwisko = nazwaUzytkownika,
                Sol = tekstSoli,
                SkrolSolonegoHasla = skrotSolonegoHasla,
                Role = role
            };
            Uzytkownicy.Add(uzytkownik.Nazwisko, uzytkownik);
            return uzytkownik;
        }

        

        public static void Zaloguj(string nazwaUzytkownika,string haslo)
        {
            if(SprawdzHaslo(nazwaUzytkownika, haslo))
            {
                var tozsamosc = new GenericIdentity(nazwaUzytkownika, "Uwierzytelnianie");
                var uprawnienia = new GenericPrincipal(tozsamosc, Uzytkownicy[nazwaUzytkownika].Role);
                System.Threading.Thread.CurrentPrincipal = uprawnienia;
            }
        }

        public static bool SprawdzHaslo(string nazwaUzytkownika, string haslo)
        {
            if(!Uzytkownicy.ContainsKey(nazwaUzytkownika))
            {
                return false;
            }
            var uzytkownik = Uzytkownicy[nazwaUzytkownika];
            var sha = SHA256.Create();
            var soloneHaslo = haslo + uzytkownik.Sol;
            var skrotSolonegoHasla = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(soloneHaslo)));
            return (skrotSolonegoHasla == uzytkownik.SkrolSolonegoHasla);

        }

        public static void FunkcjaWymagaUprawniwn()
        {
            if(Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Własność CurrentPricipal nie może być null");
            }
            if (!Thread.CurrentPrincipal.IsInRole("Administratorzy"))
            {
                throw new SecurityException("Brak uprawnien");

            }
            WriteLine("Dostęp do funkcji administratora");
        }

        public static string KluczPubliczny;
        public static string ZapiszDoXml (this RSA rsa, bool dolaczPrywatneParametry)
        {
            var p = rsa.ExportParameters(dolaczPrywatneParametry);
            XElement xml;
            if (dolaczPrywatneParametry)
            {
                xml = new XElement("KluczRSA"
                    , new XElement("Modulo", Convert.ToBase64String(p.Modulus))
                    , new XElement("Wykladnik", Convert.ToBase64String(p.Exponent))
                     , new XElement("P", Convert.ToBase64String(p.P))
                      , new XElement("Q", Convert.ToBase64String(p.Q))
                       , new XElement("DP", Convert.ToBase64String(p.DP))
                        , new XElement("DQ", Convert.ToBase64String(p.DQ))
                         , new XElement("InverseQ", Convert.ToBase64String(p.InverseQ))
                         );
            }
            else
            {
                xml = new XElement("KluczRSA"
                , new XElement("Modulo", Convert.ToBase64String(p.Modulus))
                , new XElement("Wykladnik", Convert.ToBase64String(p.Exponent)));

            }
            return xml?.ToString();
        }

        public static void CzytajZXml(this RSA rsa, string parametersAsXml)
        {
            var xml = XDocument.Parse(parametersAsXml);
            var root = xml.Element("KluczRSA");
            var p = new RSAParameters
            {
                Modulus = Convert.FromBase64String(root.Element("Modulo").Value),
                Exponent = Convert.FromBase64String(root.Element("Wykladnik").Value)
            };
            if (root.Element("P") !=null)
            {
                p.P = Convert.FromBase64String(root.Element("P").Value);
                p.Q = Convert.FromBase64String(root.Element("Q").Value);
                p.DP = Convert.FromBase64String(root.Element("DP").Value);
                p.DQ = Convert.FromBase64String(root.Element("DQ").Value);
                p.InverseQ = Convert.FromBase64String(root.Element("InverseQ").Value);
            }
            rsa.ImportParameters(p);
        }

        public static string GenerujPodpis(string dane)
        {
            byte[] bajtyDanych = Encoding.Unicode.GetBytes(dane);
            var sha = SHA256.Create();
            var skrotDanych = sha.ComputeHash(bajtyDanych);
            var rsa = RSA.Create();
            KluczPubliczny = rsa.ZapiszDoXml(false);
            return Convert.ToBase64String(rsa.SignHash(skrotDanych, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }

        public static bool KontrolaPodpisu(string dane,string podpis)
        {
            byte[] bajtyDanych = Encoding.Unicode.GetBytes(dane);
            var sha = SHA256.Create();
            var skrotDanych = sha.ComputeHash(bajtyDanych);
            byte[] bajtyPodpis = Convert.FromBase64String(podpis);
            var rsa = RSA.Create();
            rsa.CzytajZXml(parametersAsXml: KluczPubliczny);
            return rsa.VerifyHash(skrotDanych, bajtyPodpis, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }




        public static byte[] PobierzLosowyKluczWektor(int rozmiar)
        {
            var r = RandomNumberGenerator.Create();
            var dane = new byte[rozmiar];
            r.GetNonZeroBytes(dane);

            return dane;
        }




    }
       

}

