using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Modules.Create
{
    public class ModulesCreateDTO
    {

        public string Name { get; set; } = null!;

        public long CreatedBy { get; set; }

    }
}
