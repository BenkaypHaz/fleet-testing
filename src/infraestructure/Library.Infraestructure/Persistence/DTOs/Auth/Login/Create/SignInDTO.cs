using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Login.Create
{
    public class SignInDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
