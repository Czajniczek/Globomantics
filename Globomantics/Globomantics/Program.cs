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
        // Punkt wejœcia aplikacji. Metoda Main konfiguruje ASP.NET Core i uruchamia j¹
        // Aplikacja pocz¹tkowo uruchamiana jest jako aplikacja wiersza poleceñ
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); //Od tego momentu aplikacja staje siê aplikacj¹ ASP.NET Core
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => // Konfiguracja hosta sieci Web przy u¿yciu wartoœci domyœlnych
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
