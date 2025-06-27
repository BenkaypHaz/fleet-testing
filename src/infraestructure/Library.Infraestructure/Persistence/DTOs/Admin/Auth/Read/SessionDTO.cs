using Library.Infraestructure.Persistence.DTOs.Admin.UsersRoles.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Auth.Read
{
    public class SessionDTO
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public List<RolesReadDTO>? Roles { get; set; }
        public List<string>? Authorizations { get; set; }
        public bool IsActive { get; set; }
    }
}
