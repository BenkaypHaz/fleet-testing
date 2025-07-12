using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Create
{
    public class SettingFuelOrderIssuerCreateDto
    {
        public string Name { get; set; } 
        public decimal PercentageOverprice { get; set; }
        public bool IsDefault { get; set; } 
    }
}
