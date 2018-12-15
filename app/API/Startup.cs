using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using API.Infrastructure;
using DAL;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Options;

/*
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Logging;
using Model;
*/

[assembly: ApiController]
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
            #region Activate cross-origin requests
            //services.AddCors();
            #endregion

            #region DB process
            services.AddDbContext<BepwayContext>(options => {
                string connectionString = new ConfigurationHelper("BepWayConnectionString").GetConnectionString();
                options.UseSqlServer(connectionString);
            }); // externalisation de la connectionString dans appsettings.json
            #endregion

            #region Swagger/OpenAPI
            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BepWay API", Version = "v1" });
            }
            ); // utilisation de Swagger/OpenAPI avec Swashbuckle*/
            #endregion

            #region Authentification
            services.AddAuthorization(options =>
                {
                    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
                }
            );

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                Configuration.GetValue<string>("SecretKey"))
            );
            services.Configure<JwtIssuerOptions>(options => {
                options.Issuer = Configuration.GetValue<string>("Authentification.Issuer");
                options.Audience = Configuration.GetValue<string>("Authentification.Audience");
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetValue<string>("Authentification.Issuer"),

                ValidateAudience = true,
                ValidAudience = Configuration.GetValue<string>("Authentification.Audience"),

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                    options.Audience = Configuration.GetValue<string>("Authentification.Audience");
                    options.ClaimsIssuer = Configuration.GetValue<string>("Authentification.Issuer");
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.SaveToken = true;
                });
            #endregion

            #region AutoMapper
            services.AddAutoMapper();
            #endregion

            services
                .AddMvc(/*options => options.Filters.Add(typeof(BusinessExceptionFilter));*/)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            #region Cross-origin requests
            //app.UseCors(builder => builder.WithOrigins("https//www.example.com","https://example.com"));
            #endregion

            #region Swagger/OpenAPI
            /*
            app.UserSwagger();
            app.UseSwagerrUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BepWay API v1");
            });
            */
            #endregion
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
