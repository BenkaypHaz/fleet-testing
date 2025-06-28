using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderProfile.Read
{
    public class ProviderProfileReadDto
    {
        public long Id { get; set; }
        public required string BusinessName { get; set; }
        public required string LegalId { get; set; }
        public bool IsActive { get; set; }
    }
}
