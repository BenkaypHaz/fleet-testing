using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Roles.Update
{
    public class RemoveAuthorizationDTO
    {
        public long RoleId { get; set; }
        public List<long>? AuthsId { get; set; }
    }
}
