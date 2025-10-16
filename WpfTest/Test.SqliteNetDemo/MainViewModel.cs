using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Test.SqliteNetDemo.Models;
using Test.SqliteNetDemo.SQLiteNet;

namespace Test.SqliteNetDemo
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        private ObservableCollection<Stock> mStockList;

        public ObservableCollection<Stock> StockList
        {
            get { return mStockList; }
            set
            {
                mStockList = value;
                RaisePropertyChanged(nameof(StockList));
            }
        }

        private Stock mCurrentStock;

        public Stock CurrentStock
        {
            get { return mCurrentStock; }
            set
            {
                mCurrentStock = value;
                RaisePropertyChanged(nameof(CurrentStock));
            }
        }

        #endregion

        #region Commands

        public RelayCommand<Stock> AddCommand { get; private set; }
        public RelayCommand<Stock> DeleteCommand { get; private set; }
        public RelayCommand<Stock> SelectionChangedCommand { get; private set; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            mStockList = new ObservableCollection<Stock>();
            mCurrentStock = new Stock();
            AddCommand = new RelayCommand<Stock>(async s =>
            {
                if (s == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(s.LocalId))
                {
                    s.LocalId = Guid.NewGuid().ToString().Replace("-", "");
                    s.CreatedTime = DateTime.Now;
                    s.Version = 1;
                    int count = await SQLiteHelper.Instance.AddAsync<Stock>(s);
                    if (count > 0)
                    {
                        StockList.Add(s);
                    }
                }
                else
                {
                    s.UpdatedTime = DateTime.Now;
                    s.Version += 1;
                    int count = await SQLiteHelper.Instance.UpdateAsync<Stock>(s);
                    if (count > 0)
                    {
                        StockList.Remove(s);
                        StockList.Add(s);
                    }
                }
                CurrentStock = new Stock();
            });
            DeleteCommand = new RelayCommand<Stock>(async s =>
            {
                if (s == null || string.IsNullOrEmpty(s.LocalId))
                {
                    return;
                }
                s.Status = 1;
                s.UpdatedTime = DateTime.Now;
                // 逻辑删除
                int count = await SQLiteHelper.Instance.UpdateAsync<Stock>(s);
                if (count > 0)
                {
                    StockList.Remove(s);
                }
                CurrentStock = new Stock();
            });
            SelectionChangedCommand = new RelayCommand<Stock>((s) =>
            {
                if (s == null || string.IsNullOrEmpty(s.LocalId))
                {
                    return;
                }
                CurrentStock = s;
            });
        }

        #endregion

        #region Methods

        public async void GetStockList()
        {
            var stocks = await SQLiteHelper.Instance.GetAllAsync<Stock>(s => s.Status == 0);
            StockList = new ObservableCollection<Stock>(stocks);
        }

        #endregion

        #region INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
