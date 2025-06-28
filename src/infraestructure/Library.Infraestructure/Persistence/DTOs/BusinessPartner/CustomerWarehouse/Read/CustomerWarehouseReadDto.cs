using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.CustomerWarehouse.Read
{
    public partial class CustomerWarehouseReadDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string DisplayName => $"{Code} - {Name}";
        public bool IsActive { get; set; }
    }
}
