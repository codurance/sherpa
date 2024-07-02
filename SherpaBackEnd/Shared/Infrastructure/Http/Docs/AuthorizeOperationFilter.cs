using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AuthorizeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ApplySwaggerAuthenticationToControllers(operation, context);
        ApplySwaggerAuthenticationToMethods(operation, context);
    }

    private void ApplySwaggerAuthenticationToMethods(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.GetCustomAttribute(typeof(AuthorizeAttribute)) != null)
        {
            AddSwaggerAuthenticationTo(operation);
        }
    }

    private void ApplySwaggerAuthenticationToControllers(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor &&
            controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute(typeof(AuthorizeAttribute)) != null)
        {
            AddSwaggerAuthenticationTo(operation);
        }
    }

    private void AddSwaggerAuthenticationTo(OpenApiOperation operation)
    {
        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            }
        };
    }
}