using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Bam.Net.Services;
using Bam.Net.Web.AppModules;
using Bam.Net.CoreServices;
using Bam.Net.Configuration;

namespace Bam.Net.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ConfigurationResolver.Startup(configuration);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton(ApplicationServiceRegistry.Configure((appRegistry) =>
            {                
                // Configure the Bam appRegistry here
                appRegistry
                    .For<ConfigurationResolver>().Use(ConfigurationResolver.Current)
                    .For<ProxyAssemblyGeneratorService>().Use<ProxyAssemblyGeneratorServiceProxy>();

                appRegistry
                    .RegisterAppModules()
                    .AddServices(services);
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}