using System;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;

namespace AsychronicznaKonsola
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://www.apple.com/");
            WriteLine($"Strona główna Apple {response.Content.Headers.ContentLength:N0} bytes.");
        }
    }
}
