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
        public void ConfigureServices(IServiceCollection services) //Wstrzykiwanie zale¿noœci
        {
            //Mechanizm iniekcji zale¿noœci zale¿y od kontenera Inversion of Control (IoC)
            //Okres istenienia obiektu jest zarz¹dzany przez kontener
            //Czas ¿ycia instancji:
            //• Transient (przejœciowy okres istenienia) - nowe wyst¹pienie typu jest tworzone za ka¿dym razem, gdy jest o to proszone
            //• Scoped (okres istnienia z zakresem) - wyst¹pienie bêdzie istnieæ do momentu ca³kowitego obs³u¿enia ¿¹dania internetowego
            //• Singleton (pojednczy okres istnienia) - po utworzeniu wystapienia to samo wyst¹pienie bêdzie dostarczane za ka¿dym razem, 
            //  a¿ aplikacja zostanie zamkniêta

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
