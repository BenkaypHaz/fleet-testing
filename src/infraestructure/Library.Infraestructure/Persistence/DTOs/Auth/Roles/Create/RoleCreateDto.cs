using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Roles.Create
{
    public class RoleCreateDto
    {
        public required string Description { get; set; }
        public int[] Auth_Id { get; set; }
        public bool IsActive { get; set; }
    }
}
