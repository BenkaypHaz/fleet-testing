using Library.Infraestructure.Common.Attributes.Swagger;
using Library.Infraestructure.Common.ResponseHandler;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Library.Infraestructure.Common.Filters.Swagger
{
    public class EndpointDocumentationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodInfo = context.MethodInfo;
            var attribute = methodInfo.GetCustomAttribute<SwaggerDocumentationAttribute>();

            if (attribute != null)
            {
                operation.Summary = attribute.Summary;
                foreach (var statusCode in attribute.StatusCodes)
                    operation.Responses[statusCode.ToString()] = new OpenApiResponse { Description = ResponseStatusMessages.GetStatusMessageResponse(statusCode) };

            }
        }
    }
}
