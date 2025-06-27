using Library.Infraestructure.Persistence.DTOs.Auth.Users.Read;
using Library.Infraestructure.Persistence.DTOs.General.Region.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.General.City.Read
{
    public class CityReadFirstDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required RegionReadFirstDto Region { get; set; }
        public bool IsActive { get; set; }
        public AssignedUserReadFirstDto CreatedByNavigation { get; set; } = null!;
        public AssignedUserReadFirstDto? ModifiedByNavigation { get; set; }
    }
}
