using Library.Infraestructure.Persistence.DTOs.Auth.Authorizations.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read
{
    public class RoleReadFirstDto
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public IEnumerable<AuthorizationReadDto>? Authorizations { get; set; }
        public long? CreatedBy { get; set; }
        public string? CreatedByUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string? ModifiedByUserName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
