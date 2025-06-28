using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Utils.Filters
{
    public class FilterOptionsDto
    {
        private int _recordsPerPage = 10;
        private readonly int _maxRecordsPerPage = 20;
        public bool enablePagination = true;
        public int page { get; set; } = 1;
        public int recordsPerPage
        {
            get { return _recordsPerPage; }
            set { _recordsPerPage = value > _maxRecordsPerPage ? _maxRecordsPerPage : value; }
        }
        public string? searchValue { get; set; } = string.Empty;
        public int? statusId { get; set; } = null;
        public string? sortDirection { get; set; } = "desc";
        public bool? isActive { get; set; } = null;
    }
}
