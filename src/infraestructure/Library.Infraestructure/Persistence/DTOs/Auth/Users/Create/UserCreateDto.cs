using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Users.Create
{
    public class UserCreateDto
    {
        public string Dni { get; set; } = null!;
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Password { get; set; }
        public bool ResetPassword { get; set; }
        public int[] Role_Id { get; set; }
        public bool IsActive { get; set; }
    }
}
