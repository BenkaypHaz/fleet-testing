﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library.Infraestructure.Common.Filters.Swagger
{
    public class TagByApiExplorerSettingsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var apiExplorerSettings = controllerActionDescriptor
                    .ControllerTypeInfo.GetCustomAttributes(typeof(ApiExplorerSettingsAttribute), true)
                    .Cast<ApiExplorerSettingsAttribute>().FirstOrDefault();
                if (apiExplorerSettings != null && !string.IsNullOrWhiteSpace(apiExplorerSettings.GroupName))
                {
                    operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = apiExplorerSettings.GroupName } };
                }
                else
                {
                    operation.Tags = new List<OpenApiTag>
                    {new OpenApiTag {Name = controllerActionDescriptor.ControllerName}};
                }
            }
        }
    }
}
