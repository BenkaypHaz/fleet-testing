using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Roles.Create
{
    public class AddAuthorizationDTO
    {
        public long RoleId { get; set; }
        public List<Auths>? Auths { get; set; }
        public long CreatedBy { get; set; }
    }

    public class Auths
    {
        public long Id { get; set; }
        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
