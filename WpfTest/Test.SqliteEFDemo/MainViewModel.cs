using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using Test.SqliteEFDemo.Models;
using Test.SqliteEFDemo.SQLiteNet;

namespace Test.SqliteEFDemo
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
            AddCommand = new RelayCommand<Stock>(s =>
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
                    SQLiteDbContext.Instance.Stocks.Add(s);
                    SQLiteDbContext.Instance.SaveChanges();
                }
                else
                {
                    s.UpdatedTime = DateTime.Now;
                    s.Version += 1;
                    SQLiteDbContext.Instance.Stocks.AddOrUpdate(s);
                    SQLiteDbContext.Instance.SaveChanges();
                }
                CurrentStock = new Stock();
                GetStockList();
            });
            DeleteCommand = new RelayCommand<Stock>(s =>
            {
                if (s == null || string.IsNullOrEmpty(s.LocalId))
                {
                    return;
                }
                s.Status = 1;
                s.UpdatedTime = DateTime.Now;
                // 逻辑删除
                SQLiteDbContext.Instance.Stocks.AddOrUpdate(s);
                SQLiteDbContext.Instance.SaveChanges();
                CurrentStock = new Stock();
                GetStockList();
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

        public void GetStockList()
        {
            var stocks = SQLiteDbContext.Instance.Stocks.Where(s => s.Status == 0).ToList();
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
