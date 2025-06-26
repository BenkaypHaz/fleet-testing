using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Users.Update
{
    public class UsersUpdateDTO
    {
        public long Id { get; set; }

        public string Email { get; set; } = null!;

        public string Dni { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? ProfilePicture { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public DateOnly? BirthDate { get; set; }

        public string? Gender { get; set; }

        public List<long> Roles { get; set; } = new List<long>();

        public long ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
