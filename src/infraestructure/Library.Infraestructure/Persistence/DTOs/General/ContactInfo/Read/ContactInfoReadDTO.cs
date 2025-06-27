using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.General.ContactInfo.Read
{
    public class ContactInfoReadDTO
    {
        public long Id { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public long CityId { get; set; }

        public string? CityName { get; set; } = null;

        public long StateId { get; set; }

        public string? StateName { get; set; } = null;

        public string Address { get; set; } = null!;

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }
    }
}
