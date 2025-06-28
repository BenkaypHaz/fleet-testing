using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.VehicleBrand.Read
{
    public class VehicleBrandReadDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
