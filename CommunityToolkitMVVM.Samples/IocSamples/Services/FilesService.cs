using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocSamples.Services
{
    public class FilesService : IFilesService
    {
        public string GetFile(string path)
        {
            return "Hello World";
        }
    }
}
