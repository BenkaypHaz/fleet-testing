using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Update
{
    public class AuthorizationsUpdateDTO
    {
        public long Id { get; set; }

        public string? Description { get; set; }

        public long ModuleId { get; set; }

        public string RouteValue { get; set; } = null!;

        public long? ModifiedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
