using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Authorizations.Read
{
    public class ModuleReadDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public List<AuthorizationReadDto>? Authorizations { get; set; }
    }
}
