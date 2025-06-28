using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Create
{
    public partial class TransportVehicleCreateDto
    {
        public long BusinessPartnerProviderProfileId { get; set; }
        public long? BusinessPartnerProviderDriverId { get; set; }
        public required string PlateNumber { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public required string Color { get; set; }
        public required string Vin { get; set; }
        public int Axles { get; set; }
        public string? Description { get; set; }
    }
}
