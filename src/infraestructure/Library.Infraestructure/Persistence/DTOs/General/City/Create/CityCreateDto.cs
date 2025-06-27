using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.General.City.Create
{
    public class CityCreateDto
    {
        public required string Name { get; set; }
        public long RegionId { get; set; }
    }
}
