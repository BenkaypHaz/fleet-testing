using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.ShipmentFreight.Create
{
    public partial class ShipmentFreightCreateDto
    {
        public long ShipmentProjectContractId { get; set; }
        public long BusinessPartnerProviderTransportVehicleId { get; set; }
        public long CustomerWarehouseId { get; set; }
        public long BusinessPartnerProviderDriverId { get; set; }
        public long ShipmentFreightStatusId { get; set; }
        public long ShipmentFreightTypeId { get; set; }
        public long FreightProductTypeId { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string? Observations { get; set; }
    }
}
