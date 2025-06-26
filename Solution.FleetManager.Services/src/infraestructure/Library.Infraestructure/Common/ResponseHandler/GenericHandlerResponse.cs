using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Infraestructure.Common.ResponseHandler
{
    public class GenericHandlerResponse<T>
    {
        public GenericHandlerResponse(int statusCode = 400, T? data = default, int dataRecords = 0, string? CustomMessage = null, string ExceptionMessage = "")
        {
            this.statusCode = statusCode;
            this.status = statusCode >= 200 && statusCode < 300;
            this.data = data;
            this.dataRecords = dataRecords;
            this.message = CustomMessage ?? ResponseStatusMessages.GetStatusMessageResponse(statusCode);
            this.exceptionMessage = ExceptionMessage;
        }

        public int statusCode { get; set; }
        public bool status { get; set; }
        public T? data { get; set; }
        public int dataRecords { get; set; }
        public string? message { get; set; }
        public string? exceptionMessage { get; set; }
    }
}
