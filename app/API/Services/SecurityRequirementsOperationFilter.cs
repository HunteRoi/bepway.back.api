using System.Collections.Generic;
using System.Linq;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(Operation operation, OperationFilterContext context)
    {
        // Policy names map to scopes
        var requiredScopes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Select(attr => attr.Policy)
            .Distinct();

        if (requiredScopes.Any())
        {
            operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            operation.Security.Add(new Dictionary<string, IEnumerable<string>> { { "Bearer", requiredScopes }
            });
        }
    }
}