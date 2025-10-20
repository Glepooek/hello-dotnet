using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommandSamples.ViewModels
{
    internal class ViewModelLocator
    {
        private IServiceProvider ServiceProvider { get; }
        public ViewModelLocator()
        {
            var services = new ServiceCollection();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainCanExecutedViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public MainViewModel MainViewModel => ServiceProvider.GetRequiredService<MainViewModel>();
        public MainCanExecutedViewModel MainCanExecutedViewModel => ServiceProvider.GetRequiredService<MainCanExecutedViewModel>();
    }
}
