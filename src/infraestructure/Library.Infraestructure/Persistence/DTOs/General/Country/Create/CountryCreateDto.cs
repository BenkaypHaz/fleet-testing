﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.General.Country.Create
{
    public class CountryCreateDto
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string ISO_Code { get; set; }
    }
}
