using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.DTOs.Utils.Files
{
    public class FileTypeDTO
    {
        public byte[] File { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
        public int MediaType { get; set; }
    }
}
