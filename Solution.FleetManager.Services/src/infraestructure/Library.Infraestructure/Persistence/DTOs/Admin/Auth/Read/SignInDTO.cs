using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Auth.Read
{
    public class SignInDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
