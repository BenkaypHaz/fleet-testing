using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Setting.FreightPricing.Read
{
    public class FreightPricingSuggestedPriceReadDto
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public long WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public long DispatchBranchId { get; set; }
        public string DispatchBranchName { get; set; } = string.Empty;
        public decimal? SuggestedPrice { get; set; }
        public decimal? Cost { get; set; }
    }
}