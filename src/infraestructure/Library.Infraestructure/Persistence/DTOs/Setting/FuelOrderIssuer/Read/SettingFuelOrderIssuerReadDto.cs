using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Read
{
    public class SettingFuelOrderIssuerReadDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal PercentageOverprice { get; set; }
        public bool IsDefault { get; set; }
    }
}
