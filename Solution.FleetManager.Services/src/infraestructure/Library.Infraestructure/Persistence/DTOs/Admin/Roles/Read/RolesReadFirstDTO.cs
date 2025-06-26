using Library.Infraestructure.Persistence.DTOs.Admin.Authorizations.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Roles.Read
{
    public class RolesReadFirstDTO
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public List<AuthorizationsReadDTO>? Authorizations { get; set; }

        public long CreatedBy { get; set; }

        public string? CreatedByName { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public string? ModifiedByName { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
