using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics
{
    public class Program
    {
        // Punkt wej�cia aplikacji. Metoda Main konfiguruje ASP.NET Core i uruchamia j�
        // Aplikacja pocz�tkowo uruchamiana jest jako aplikacja wiersza polece�
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); //Od tego momentu aplikacja staje si� aplikacj� ASP.NET Core
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => // Konfiguracja hosta sieci Web przy u�yciu warto�ci domy�lnych
                {
                    webBuilder.UseUrls("https://localhost:5001");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
