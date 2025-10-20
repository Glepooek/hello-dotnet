using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RelayCommandSamples.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            //IncreaseCounterCommand = new RelayCommand(IncreaseCounter);
            DownloadTextCommand = new AsyncRelayCommand(GetResultAsync);
        }

        //private int mCounter;
        //public int Counter
        //{
        //    get => mCounter;
        //    set => SetProperty(ref mCounter, value);
        //}

        [ObservableProperty]
        private int counter;

        private string result;
        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        //public ICommand IncreaseCounterCommand { get; private set; }
        public IAsyncRelayCommand DownloadTextCommand { get; private set; }

        [RelayCommand]
        public void IncreaseCounter() => Counter++;

        private async Task<string> DownloadTextAsync()
        {
            await Task.Delay(5000);
            return await Task.FromResult("test");
        }

        private async Task GetResultAsync()
        {
            Result = string.Empty;
            Result = await DownloadTextAsync();
        }
    }
}
