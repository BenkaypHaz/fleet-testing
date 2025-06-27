using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Login.Read
{
    public class JwtSessionDto
    {
        public long Id { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        public IEnumerable<string>? Authorizations { get; set; }
    }
}
