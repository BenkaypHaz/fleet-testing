using Library.Infraestructure.Common.Helpers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library.Infraestructure.Common.Filters.Swagger
{
    public class BasePathDocumentFilter : IDocumentFilter
    {
        private readonly int _localhostPort;
        private readonly string _path;
        public BasePathDocumentFilter(int localhostPort, string path)
        {
            _localhostPort = localhostPort;
            _path = path;
        }
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            string SERVICES_HOST = BaseHelper.GetEnvVariable("SERVICES_HOST");
            swaggerDoc.Servers = new List<OpenApiServer>() {
                new OpenApiServer() { Url = $"{SERVICES_HOST}/{_path}" },
                new OpenApiServer() { Url = $"http://localhost:{_localhostPort}" }
            };
        }
    }
}
