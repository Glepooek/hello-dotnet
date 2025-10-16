using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocSamples.Services
{
    public interface IFilesService
    {
        string GetFile(string path);
    }
}
