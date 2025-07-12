using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Update
{
    public class SettingFuelOrderIssuerUpdateDto
    {
        public string Name { get; set; } = null!;
        public decimal PercentageOverprice { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}
