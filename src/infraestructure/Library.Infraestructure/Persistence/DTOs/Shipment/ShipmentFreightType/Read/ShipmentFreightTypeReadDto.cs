﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Shipment.ShipmentFreightType.Read
{
    public class ShipmentFreightTypeReadDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
