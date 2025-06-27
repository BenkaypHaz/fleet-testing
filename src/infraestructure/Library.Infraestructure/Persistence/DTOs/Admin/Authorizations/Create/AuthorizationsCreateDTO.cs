using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Create
{
    public class AuthorizationsCreateDTO
    {
        public string? Description { get; set; }

        public long ModuleId { get; set; }

        public string RouteValue { get; set; } = null!;

        public long CreatedBy { get; set; }
    }
}
