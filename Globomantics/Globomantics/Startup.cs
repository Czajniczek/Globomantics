using Globomantics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // Metoda odpowiadaj�ca za wstrzykiwanie zale�no�ci
        public void ConfigureServices(IServiceCollection services)
        {
            // Mechanizm iniekcji zale�no�ci zale�y od kontenera Inversion of Control (IoC)
            // Okres istenienia obiektu jest zarz�dzany przez kontener
            // Czas �ycia instancji:
            // � Transient (przej�ciowy okres istenienia) - nowe wyst�pienie typu jest tworzone za ka�dym razem, gdy jest o to proszone
            // � Scoped (okres istnienia z zakresem) - wyst�pienie b�dzie istnie� do momentu ca�kowitego obs�u�enia ��dania internetowego
            // � Singleton (pojednczy okres istnienia) - po utworzeniu wystapienia to samo wyst�pienie b�dzie dostarczane za ka�dym razem, 
            //   a� aplikacja zostanie zamkni�ta

            services.AddControllersWithViews();

            // Za ka�dym razem, gdy jaki� typ prosi o obiekt IConferenceService, podaj wyst�pienie ConferenceApiService
            services.AddSingleton<IConferenceService, ConferenceApiService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        // Metoda konfiguruj�ca potok ��da� HTTP ASP.NET Core. Potok okre�la, w jaki spos�b aplikacja b�dzie odpowiada� na ��dania HTTP.
        // Poszczeg�lne cz�ci, kt�re tworz� potok, nazywane s� oprogramowaniem po�rednicz�cym (Middleware)
        // Przyk�ad: Auth => MVC => Static Files
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
