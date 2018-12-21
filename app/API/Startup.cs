using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using API.Infrastructure;
using API.Services;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

[assembly : ApiController]
namespace API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            ConfigurationHelper helper = new ConfigurationHelper ("secrets.json");

            #region CORS config
            services.AddCors();
            #endregion

            #region DB process
            services.AddDbContext<BepwayContext> (options => {
                string connectionString = helper.Get ("BepWayConnectionString");
                options.UseSqlServer (connectionString);
            });
            #endregion

            #region Swagger/OpenAPI
            services.AddSwaggerDocumentation ();
            #endregion

            #region Authentification
            string Issuer = helper.Get ("Authentication:Issuer");
            string Audience = helper.Get ("Authentication:Audience");

            services.AddAuthorization (options => {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder (JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser ();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build ();
            });

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (helper.Get ("SecretKey")));
            services.Configure<JwtIssuerOptions> (options => {
                options.Issuer = Issuer;
                options.Audience = Audience;
                options.SigningCredentials = new SigningCredentials (_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters {
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
                .AddAuthentication (options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (JwtBearerDefaults.AuthenticationScheme, options => {
                    options.Audience = Audience;
                    options.ClaimsIssuer = Issuer;
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.SaveToken = true;
                });
            #endregion

            #region AutoMapper
            services.AddAutoMapper ();
            #endregion

            services
                .AddMvc (options => {
                    options.Filters.Add (typeof (BusinessExceptionFilter));
                })
                .AddJsonOptions (options => options.SerializerSettings.Formatting = Formatting.Indented)
                .SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            #region CORS config
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                
            });
            #endregion

            #region Swagger/OpenAPI
            app.UseSwaggerDocumentation ();
            #endregion

            #region Global exceptions handler
            app.UseExceptionHandler (errorApp => {
                errorApp.Run (async context => {
                    await Task.Run (() => {
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature> ();
                        var exception = errorFeature.Error;

                        var problemDetails = new ProblemDetails {
                            Instance = $"urn:bepway:error:{Guid.NewGuid()}"
                        };

                        if (exception is BadHttpRequestException badHttpRequestException) {
                            problemDetails.Title = "Invalid request";
                            problemDetails.Status = (int) typeof (BadHttpRequestException)
                                .GetProperty ("StatusCode", BindingFlags.NonPublic | BindingFlags.Instance)
                                .GetValue (badHttpRequestException);
                            problemDetails.Detail = badHttpRequestException.Message;
                        } else {
                            problemDetails.Title = "An unexpected error occurred!";
                            problemDetails.Status = 500;
                            problemDetails.Detail =
                                /*context.Request.IsTrusted()
                                                           ? */
                                exception.ToString ()
                            /*: "Wow that's an interesting error you've got there!"*/
                            ;
                        }

                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.WriteJson (problemDetails);
                    });
                });
            });
            #endregion

            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}