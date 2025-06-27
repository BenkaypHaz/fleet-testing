using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Auth.Read
{
    public class ValidateUserResetPasswordTokenDTO
    {
        public long Id { get; set; }

        public int Token { get; set; }
    }
}
