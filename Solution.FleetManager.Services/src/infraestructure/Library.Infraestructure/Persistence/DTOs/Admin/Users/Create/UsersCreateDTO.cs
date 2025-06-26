using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Users.Create
{
    public class UsersCreateDTO
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string Password { get; set; } = null!;
        public int[]? Roles { get; set; }
        public long CreatedBy { get; set; }
    }
}
