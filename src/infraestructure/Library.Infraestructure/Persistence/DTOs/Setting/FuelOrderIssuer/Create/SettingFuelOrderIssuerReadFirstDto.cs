using Library.Infraestructure.Persistence.DTOs.Auth.Users.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Create
{
    public class SettingFuelOrderIssuerReadFirstDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal PercentageOverprice { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public AssignedUserReadFirstDto CreatedByNavigation { get; set; } = null!;
        public AssignedUserReadFirstDto? ModifiedByNavigation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
