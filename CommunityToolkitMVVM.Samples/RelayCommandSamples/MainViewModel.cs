using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RelayCommandSamples
{
    internal class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            IncrementCounterCommand = new RelayCommand(IncrementCounter);
            DownloadCommand = new AsyncRelayCommand(DownloadText);
        }

        private int mCounter;
        public int Counter
        {
            get => mCounter;
            set => SetProperty(ref mCounter, value);
        }

        public ICommand IncrementCounterCommand { get; private set; }
        public IAsyncRelayCommand DownloadCommand { get; private set; }

        private void IncrementCounter() => Counter++;

        private Task<string> DownloadText()
        {
            return Task.FromResult("test");
        }
    }
}
