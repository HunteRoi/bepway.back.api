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
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentification.JwtBearer;
//using Microsft.IdentityModel.Tokens;
//using Swashbuckle.AspNetCore.Swagger;
//using API.Infrastructure;
using AutoMapper;
//using Model;
using DAL;

namespace API
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
            services.AddDbContext<BepwayContext>(options => {
                string connectionString = new ConfigurationHelper("BepWayConnectionString").GetConnectionString();
                options.UseSqlServer(connectionString);
            }); // externalisation de la connectionString dans appsettings.json
            
            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BepWay API", Version = "v1" });
            }
            ); // utilisation de Swagger/OpenAPI avec Swashbuckle*/
            
            // authentification (access_token)

            services.AddAutoMapper();// AutoMapper (avec un profil)
            
            services.AddMvc(/*options => {
                options.Filters.Add(typeof(BusinessExceptionFilter));
            }*/).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            /*
            app.UserSwagger();
            app.UseSwagerrUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BepWay API v1");
            });
            */
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
