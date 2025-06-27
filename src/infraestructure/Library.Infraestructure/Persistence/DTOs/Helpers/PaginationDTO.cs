using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Helpers
{
    public class PaginationDTO
    {
        private int recordsPerPage = 10;
        private readonly int maxRecordsPerPage = 100;

        public int Page { get; set; } = 1;
        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set { recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value; }
        }
        public string SortDirection { get; set; } = "DESC";
        public bool? Status { get; set; } = null;
        public string? SearchValue { get; set; } = string.Empty;
    }
}
