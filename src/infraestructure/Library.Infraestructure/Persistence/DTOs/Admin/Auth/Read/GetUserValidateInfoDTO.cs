using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Admin.Auth.Read
{
    public class GetUserValidateInfoDTO
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
