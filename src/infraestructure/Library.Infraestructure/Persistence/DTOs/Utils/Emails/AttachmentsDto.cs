using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Utils.Emails
{
    public class AttachmentsDto
    {
        public string FileName { get; set; } = null!;
        public byte[] File { get; set; } = null!;
    }
}
