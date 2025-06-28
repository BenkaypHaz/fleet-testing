using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.BusinessPartner.ProviderDriver.Read
{
    public class ProviderDriverReadDto
    {
        public long Id { get; set; }
        public string? NationalId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string DisplayName => !string.IsNullOrEmpty(NationalId) ? $"{FullName} - {NationalId}" : FullName;
        public bool IsActive { get; set; }
    }
}
