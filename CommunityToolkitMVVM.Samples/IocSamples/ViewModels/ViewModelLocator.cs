using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocSamples.ViewModels
{
    public class ViewModelLocator
    {
        #region Constructor

        public ViewModelLocator() { }

        #endregion

        #region Properties

        public MainViewModel MainViewModel => Ioc.Default.GetService<MainViewModel>();

        #endregion

        #region Methods

        #endregion
    }
}
