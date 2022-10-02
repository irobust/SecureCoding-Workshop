using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Globomantics.ProductsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var allowedOrigin = Configuration.GetValue<string>("AllowOrigin") ?? "";

            services.AddCors(options => {
                options.AddPolicy("AllowAnyOrigin", builder => builder.WithOrigins(allowedOrigin).SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

                options.AddPolicy("PrivateApi", builder => builder.WithOrigins(allowedOrigin).WithMethods("POST", "PUT", "PATCH", "DELETE", "OPTIONS").WithHeaders("Content-Type", "Authorization"));

                options.AddPolicy("PublicApi", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

                options.AddPolicy("AllowSubDomain", builder => builder.WithOrigins("http://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains());

                options.AddPolicy("AllowMultipleDomain", builder => builder.SetIsOriginAllowed(checkWhitelistingDomain));

            });
            services.AddControllers();
        }

        private static bool checkWhitelistingDomain(string host)
        {
            var corsOriginAllowed = new[] { "example.com", "demo.com", "globalmantics" };
            return corsOriginAllowed.Any(origin => host.Contains(origin));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("PrivateApi");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
