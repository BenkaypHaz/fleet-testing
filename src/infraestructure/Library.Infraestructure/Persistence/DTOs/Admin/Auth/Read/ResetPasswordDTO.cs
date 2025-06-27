using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Auth.Read
{
    public class ResetPasswordDTO
    {
        public required int token { get; set; }
        public required long Id { get; set; }
        public required string password { get; set; }
    }
}
