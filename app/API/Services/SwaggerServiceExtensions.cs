using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace API.Services
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info 
                { 
                    Version = "v1",
                    Title = "BepWay API", 
                    Description = "This is the Bepway API documentations. Find out more at [Github.com](https://github.com/HunteRoi/Smartcity2018-2019_API). Note that you will not be able to perfom tests without a valid token.\nTokens are received through the request at `api/jwt`.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Email = "tinael.devresse.01@student.henallux.be"
                    }
                });

                c.OrderActionsBy(apiDesc => $"{apiDesc.HttpMethod}_{apiDesc.ActionDescriptor.RouteValues["controller"]}");

                c.DescribeAllParametersInCamelCase();

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Type = "apiKey",
                    Description = "JWT **Authorization** header using the Bearer scheme. Example: `Bearer {token}`",
                    Name = "Authorization"
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            }
            );
 
            return services;
        }
 
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Example);
                c.DefaultModelsExpandDepth(-1);
                c.EnableDeepLinking();
                
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BepWay API - v1");
                c.RoutePrefix = String.Empty;
                c.DocumentTitle = "BepWay API - Docs";
                c.DocExpansion(DocExpansion.List);
            });
 
            return app;
        }
    }
}