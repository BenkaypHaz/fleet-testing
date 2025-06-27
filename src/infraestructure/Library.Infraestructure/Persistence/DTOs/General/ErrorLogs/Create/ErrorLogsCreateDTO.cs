using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.General.ErrorLogs.Create
{
    public class ErrorLogsCreateDTO
    {
        public string? ProjectName { get; set; }
        public string? ClassName { get; set; }
        public string? MethodName { get; set; }
        public int LineNumber { get; set; }
        public string? Description { get; set; }
        public DateTime ExceptionDate { get; set; }
    }
}
