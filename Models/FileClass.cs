using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopIt.Models
{

    public class FileClass
    {
        public int FileId { get; set; } = 0;

        public string Name { get; set; } = "";

        public string path { get; set; } = "";

        public List<FileClass> Files { get; set; } = new List<FileClass>();
    }
}
