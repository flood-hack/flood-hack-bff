using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using flood_hackathon.DataAccess;
using flood_hackathon.Models;
using flood_hackathon.Services;

namespace flood_hackathon
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.Configure<CorsOptions>(options =>
            {
                options.AddPolicy("AllowCredentials", b =>
                    b.WithOrigins("*", "https://localhost:4200", "https://flood-hack.azurewebsites.net/")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                );
            });

            services.Configure<SearchIndexSettings>(Configuration.GetSection("SearchService"));
            services.AddScoped<ToolsService>();
            services.AddSingleton<ISearchIndex, SearchIndex>();
            services.AddSingleton<ISearchAdapter, SearchAdapter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowCredentials");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
