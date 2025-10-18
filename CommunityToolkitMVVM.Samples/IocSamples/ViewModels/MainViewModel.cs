using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocSamples.Models;
using IocSamples.Services;
using Microsoft.Extensions.Options;

namespace IocSamples.ViewModels
{
    public class MainViewModel
    {
        private readonly IFilesService filesService;

        // 构造函数注入
        public MainViewModel(IFilesService service, LoggingSettings options)
        {
            filesService = service;
            FileName = service.GetFile("test");
        }

        public string FileName { get; set; }
    }
}
