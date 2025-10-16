using RefitDemo.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RefitDemo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IMainService _mainService;
        // 可在此处通过构造函数注入服务
        public MainWindowViewModel(IMainService mainService)
        {
            // 依赖注入服务可在此初始化
            _mainService = mainService ?? throw new ArgumentNullException(nameof(mainService));
            _mainService.Execute();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
