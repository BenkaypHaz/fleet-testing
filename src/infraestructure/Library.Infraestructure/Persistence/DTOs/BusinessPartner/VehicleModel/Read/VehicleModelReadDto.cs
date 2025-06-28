using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleModel.Read
{
    public class VehicleModelReadDto
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
