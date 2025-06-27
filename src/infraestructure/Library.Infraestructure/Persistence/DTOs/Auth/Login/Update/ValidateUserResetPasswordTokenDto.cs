using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Login.Update
{
    public class ValidateUserResetPasswordTokenDto
    {
        public long Id { get; set; }
        public int Token { get; set; }
    }
}
