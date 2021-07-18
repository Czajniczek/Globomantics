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
    // Klasa konfiguruj¹ca aplikacjê
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // Metoda odpowiadaj¹ca za wstrzykiwanie zale¿noœci (Dependency Injection)
        public void ConfigureServices(IServiceCollection services)
        {
            // Mechanizm iniekcji zale¿noœci zale¿y od kontenera Inversion of Control (IoC)
            // Zwykle podczas uruchamiana aplikacji typy takie jak klasa s¹ rejestrowane w kontenerze
            // W warunkach iniekcji zale¿noœci (Dependency Injection) te typy nazywane s¹ us³ugami
            // Po zarejestrowaniu inne typy mog¹ poprosiiæ kontener o wyst¹pienie tego typu

            // Okres istenienia obiektu jest zarz¹dzany przez kontener
            // Czas ¿ycia instancji:
            // • Transient (przejœciowy okres istnienia) - nowe wyst¹pienie typu jest tworzone za ka¿dym razem, gdy jest o to proszone
            // • Scoped (okres istnienia z zakresem) - wyst¹pienie bêdzie istnieæ do momentu ca³kowitego obs³u¿enia ¿¹dania internetowego
            // • Singleton (pojednczy okres istnienia) - po utworzeniu wystapienia to samo wyst¹pienie bêdzie dostarczane za ka¿dym razem, 
            //   a¿ aplikacja zostanie zamkniêta

            services.AddControllersWithViews();

            // Za ka¿dym razem, gdy jakiœ typ prosi o obiekt IConferenceService, podaj wyst¹pienie ConferenceMemoryService
            services.AddSingleton<IConferenceService, ConferenceMemoryService>();
            services.AddSingleton<IProposalService, ProposalMemoryService>();

            services.Configure<GlobomanticsOptions>(configuration.GetSection("Globomantics"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        // Metoda konfiguruj¹ca potok ¿¹dañ HTTP ASP.NET Core. Potok okreœla, w jaki sposób aplikacja bêdzie odpowiadaæ na ¿¹dania HTTP
        // Poszczególne czêœci, które tworz¹ potok, nazywane s¹ oprogramowaniem poœrednicz¹cym (Middleware)
        // Przyk³ad: Auth => MVC => Static Files
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Tryb programisty, wyœwietlaj¹cy stronê internetow¹ ze szczegó³ami b³êdu, je¿eli istnieje nieobs³ugiwany wyj¹tek
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Oprogramowanie poœrednicz¹ce do plików statycznych
            app.UseStaticFiles();

            // Uwierzytelnianie - Understanding ASP.NET Core Security
            //app.UseAuthentication();

            // Przekierowanie ¿¹dañ HTTP do ¿¹dañ HTTPS, aby zmusiæ urzytkowników do korzystania z protoko³u TLS
            app.UseHttpsRedirection();

            // Metoda sprawdzaj¹ca zarejestrowane punkty koñcowe
            app.UseRouting();

            // Metoda s³u¿¹ca do rejestrowania punktów koñcowych
            app.UseEndpoints(endpoints =>
            {
                // Dziêki MapGet mo¿emy mapowaæ wzglêdny adres URL bezpoœrednio na lambdê, która okreœla co siê stanie, je¿eli ta œcie¿ka
                // zostanie trafiona
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                // Dziêki MapControllerRoute mo¿na okreœliæ szablon trasy
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
