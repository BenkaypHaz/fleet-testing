using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.UsersRoles.Read
{
    public class RolesReadDTO
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
