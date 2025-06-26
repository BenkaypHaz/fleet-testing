using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Roles.Update
{
    public class RolesUpdateDTO
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public long? ModifiedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
