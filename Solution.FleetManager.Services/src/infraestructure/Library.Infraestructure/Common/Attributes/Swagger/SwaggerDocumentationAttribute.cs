using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Common.Attributes.Swagger
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SwaggerDocumentationAttribute : Attribute
    {
        public string Summary { get; }
        public int[] StatusCodes { get; }

        public SwaggerDocumentationAttribute(string summary, params int[] statusCodes)
        {
            Summary = summary;
            StatusCodes = statusCodes;
        }
    }
}
