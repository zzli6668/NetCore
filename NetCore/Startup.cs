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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;
using NetCore.Models;
using NetCore.Services;

namespace NetCore
{
    //This is testc!
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
            services.Configure<BusinessDatabaseSettings>(
        Configuration.GetSection(nameof(BusinessDatabaseSettings)));

            services.AddSingleton<IBusinessDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BusinessDatabaseSettings>>().Value);

            services.AddSingleton<BusinessService>();

            services.AddCors();

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
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                         .AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials());
            app.UseMvc();
        }
    }
}
