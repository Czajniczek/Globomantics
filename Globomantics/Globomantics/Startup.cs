using Globomantics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics
{
    // Klasa konfiguruj�ca aplikacj�
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // Metoda odpowiadaj�ca za wstrzykiwanie zale�no�ci (Dependency Injection)
        public void ConfigureServices(IServiceCollection services)
        {
            // Mechanizm iniekcji zale�no�ci zale�y od kontenera Inversion of Control (IoC)
            // Zwykle podczas uruchamiana aplikacji typy takie jak klasa s� rejestrowane w kontenerze
            // W warunkach iniekcji zale�no�ci (Dependency Injection) te typy nazywane s� us�ugami
            // Po zarejestrowaniu inne typy mog� poprosii� kontener o wyst�pienie tego typu

            // Okres istenienia obiektu jest zarz�dzany przez kontener
            // Czas �ycia instancji:
            // � Transient (przej�ciowy okres istnienia) - nowe wyst�pienie typu jest tworzone za ka�dym razem, gdy jest o to proszone
            // � Scoped (okres istnienia z zakresem) - wyst�pienie b�dzie istnie� do momentu ca�kowitego obs�u�enia ��dania internetowego
            // � Singleton (pojednczy okres istnienia) - po utworzeniu wystapienia to samo wyst�pienie b�dzie dostarczane za ka�dym razem, 
            //   a� aplikacja zostanie zamkni�ta

            services.AddControllersWithViews();

            // Za ka�dym razem, gdy jaki� typ prosi o obiekt IConferenceService, podaj wyst�pienie ConferenceApiService
            services.AddSingleton<IConferenceService, ConferenceMemoryService>();
            services.AddSingleton<IProposalService, ProposalMemoryService>();

            services.Configure<GlobomanticsOptions>(configuration.GetSection("Globomantics"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        // Metoda konfiguruj�ca potok ��da� HTTP ASP.NET Core. Potok okre�la, w jaki spos�b aplikacja b�dzie odpowiada� na ��dania HTTP
        // Poszczeg�lne cz�ci, kt�re tworz� potok, nazywane s� oprogramowaniem po�rednicz�cym (Middleware)
        // Przyk�ad: Auth => MVC => Static Files
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Tryb programisty, wy�wietlaj�cy stron� internetow� ze szczeg�ami b��du, je�eli istnieje nieobs�ugiwany wyj�tek
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Oprogramowanie po�rednicz�ce do plik�w statycznych
            app.UseStaticFiles();

            // Uwierzytelnianie - Understanding ASP.NET Core Security
            //app.UseAuthentication();

            // Przekierowanie ��da� HTTP do ��da� HTTPS, aby zmusi� urzytkownik�w do korzystania z protoko�u TLS
            app.UseHttpsRedirection();

            // Metoda sprawdzaj�ca zarejestrowane punkty ko�cowe
            app.UseRouting();

            // Metoda s�u��ca do rejestrowania punkt�w ko�cowych
            app.UseEndpoints(endpoints =>
            {
                // Dzi�ki MapGet mo�emy mapowa� wzgl�dny adres URL bezpo�rednio na lambd�, kt�ra okre�la co si� stanie, je�eli ta �cie�ka
                // zostanie trafiona
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                // Dzi�ki MapControllerRoute mo�na okre�li� szablon trasy
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
