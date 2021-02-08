using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Refit;
using Salesforce.Docomotion.Api;
using Salesforce.Docomotion.Authorization;
using Salesforce.Docomotion.Configuration;
using Salesforce.Docomotion.Extensions;
using Salesforce.Docomotion.Services;
using Salesforce.Docomotion.Utils.FileGenerator;

namespace Salesforce.Docomotion
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
            services.AddControllers();
            services.AddSingleton(sp => sp.GetConfigSection<SalesForceAuthConfig>());
            services.AddTransient<ITenantProvider, TenantProvider>();
            services.AddTransient<IAuthTokenStore, AuthTokenStore>();
            services.AddTransient<AuthHeaderHandler>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddRefitClient<ISalesForceApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://docomotion5-dev-ed.my.salesforce.com/services"))
                .AddHttpMessageHandler<AuthHeaderHandler>();

            services.AddSingleton<IFileGeneratorFactory, FileGeneratorFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            JsonConvert.DefaultSettings =
                () => new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
        }
    }
}
