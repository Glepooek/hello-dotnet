using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocSamples.Models;
using IocSamples.Services;
using Microsoft.Extensions.Options;
using Serilog;

namespace IocSamples.ViewModels
{
    public class MainViewModel
    {
        private readonly IFilesService filesService;
        private readonly ILogger logger;

        // 构造函数注入
        public MainViewModel(IFilesService service, IOptionsMonitor<LoggingSettings> options, ILogger logger)
        {
            this.filesService = service;
            this.logger = logger;

            FileName = service.GetFile("test");  
            logger.Information("MainViewModel created with LoggingSettings: {@LoggingSettings}", options.CurrentValue);
        }

        public string FileName { get; set; }
    }
}
