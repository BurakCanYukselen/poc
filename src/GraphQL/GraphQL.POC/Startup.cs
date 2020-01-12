using GraphiQl;
using GraphQL.POC.GrapgQL.Models;
using GraphQL.POC.GrapgQL.Queries;
using GraphQL.POC.GrapgQL.Schemas;
using GraphQL.POC.Repo;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;

namespace GraphQL.POC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(p => p.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddHttpClient<ICountryHttpClient, CountryHttpClient>(p => p.BaseAddress = new Uri("https://restcountries.eu/"));


            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDependencyResolver>(p => new FuncDependencyResolver(p.GetRequiredService));
            services.AddScoped<CountryQuery>();
            services.AddScoped<CountryType>();
            services.AddScoped<ISchema, CountrySchema>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseGraphiQl("/graph-ql-interface");
        }
    }
}
