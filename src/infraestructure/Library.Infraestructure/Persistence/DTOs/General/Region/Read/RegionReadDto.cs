using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.General.Region.Read
{
    public class RegionReadDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public long CountryId { get; set; }
        public bool IsActive { get; set; }
    }
}
