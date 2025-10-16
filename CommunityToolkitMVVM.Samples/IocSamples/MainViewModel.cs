using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocSamples.Services;

namespace IocSamples
{
    internal class MainViewModel
    {
        private readonly IFilesService filesService;

        // 构造函数注入
        public MainViewModel(IFilesService service)
        {
            filesService = service;
            FileName = service.GetFile("test");
        }

        public string FileName { get; set; }
    }
}
