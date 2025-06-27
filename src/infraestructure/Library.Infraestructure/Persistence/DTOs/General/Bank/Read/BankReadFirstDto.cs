using Library.Infraestructure.Persistence.DTOs.Auth.Users.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.General.Bank.Read
{
    public class BankReadFirstDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public AssignedUserReadFirstDto CreatedByNavigation { get; set; } = null!;
        public AssignedUserReadFirstDto? ModifiedByNavigation { get; set; }
    }
}
