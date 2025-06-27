using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Infraestructure.Common.ResponseHandler
{
    public class GenericResponseHandler<T>
    {
        public GenericResponseHandler(int statusCode = 400, T? data = default, int dataRecords = 0, string? message = null, string exceptionMessage = "")
        {
            this.statusCode = statusCode;
            this.status = statusCode >= 200 && statusCode < 300;
            this.data = data;
            this.dataRecords = dataRecords;
            this.message = message ?? ResponseStatusMessages.GetStatusMessageResponse(statusCode);
            this.exceptionMessage = exceptionMessage;
        }

        public int statusCode { get; set; }
        public bool status { get; set; }
        public T? data { get; set; }
        public int dataRecords { get; set; }
        public string? message { get; set; }
        public string? exceptionMessage { get; set; }
    }
}
