using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Utils.Emails
{
    public class ToEmailsDto
    {
        public string NameRecipient { get; set; } = null!;
        public string EmailRecipient { get; set; } = null!;
    }
}
