using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.TransportVehicle.Read
{
    public partial class TransportVehicleReadDto
    {
        public long Id { get; set; }
        public required string PlateNumber { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public string DisplayName => $"{PlateNumber} - {Brand} {Model} {Year}";
        public bool IsActive { get; set; }
    }
}
