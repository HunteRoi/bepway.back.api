using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using API.Services;
using DAL;
using AutoMapper;
using API.Infrastructure;
using Newtonsoft.Json;

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
            ConfigurationHelper helper = new ConfigurationHelper("secrets.json");

            #region DB process
            services.AddDbContext<BepwayContext>(options => {
                string connectionString = helper.Get("BepWayConnectionString");
                options.UseSqlServer(connectionString);
            });
            #endregion

            #region Swagger/OpenAPI
            services.AddSwaggerDocumentation();
            #endregion

            #region Authentification
            string Issuer = helper.Get("Authentication:Issuer");
            string Audience = helper.Get("Authentication:Audience");

            services.AddAuthorization(options =>
                {
                    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
                }
            );

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(helper.Get("SecretKey")));
            services.Configure<JwtIssuerOptions>(options => {
                options.Issuer = Issuer;
                options.Audience = Audience;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,

                ValidateAudience = true,
                ValidAudience = Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                    options.Audience = Audience;
                    options.ClaimsIssuer = Issuer;
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.SaveToken = true;
                });
            #endregion

            #region AutoMapper
            services.AddAutoMapper();
            #endregion

            services
                .AddMvc(options => 
                {
                    options.Filters.Add(typeof(BusinessExceptionFilter));
                })
                .AddJsonOptions(options => options.SerializerSettings.Formatting = Formatting.Indented)
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

            #region Swagger/OpenAPI
            app.UseSwaggerDocumentation();
            #endregion
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
