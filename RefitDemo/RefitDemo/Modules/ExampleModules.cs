using Microsoft.Extensions.DependencyInjection;
using RefitDemo.Common.Modules;
using System;
using System.Diagnostics;

namespace RefitDemo.Modules
{
    [DependsOn(typeof(ExampleDependencyModule))]
    public class ExampleMainModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMainService, MainService>();
            services.AddSingleton<MainWindowViewModel>();
        }
    }

    public class ExampleDependencyModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDependencyService, DependencyService>();
        }
    }

    public interface IMainService
    {
        void Execute();
    }

    public class MainService : IMainService
    {
        private readonly IDependencyService _dependencyService;

        public MainService(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }

        public void Execute()
        {
            Debug.WriteLine("MainService executing...");
            _dependencyService.PerformTask();
        }
    }

    public interface IDependencyService
    {
        void PerformTask();
    }

    public class DependencyService : IDependencyService
    {
        public void PerformTask()
        {
            Debug.WriteLine("DependencyService task performed.");
        }
    }
}