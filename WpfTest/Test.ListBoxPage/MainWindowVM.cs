using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Test.ListBoxPage
{
    public class MainWindowVM : LoadDataBaseVM
    {
        int count = 0;

        private ObservableCollection<DataModel> datas;
        public ObservableCollection<DataModel> Datas
        {
            get { return datas; }
            set { datas = value; }
        }

        public MainWindowVM()
        {
            Datas = new ObservableCollection<DataModel>(GetData());
        }

        internal override void LoadData()
        {
            // 模拟请求数据
            Task.Run(() =>
            {
                Thread.Sleep(1000);

                DispatcherHelper.CheckBeginInvokeOnUI(new Action(() =>
                {
                    foreach (var item in GetData())
                    {
                        Datas.Add(item);
                    }
                    LoadEnd++;
                    // 模拟加载4次后数据加载完成
                    if (LoadEnd == 4)
                    {
                        Completed = true;
                    }
                }));
            });
        }


        /// <summary>
        /// 生成模拟数据
        /// </summary>
        /// <returns></returns>
        private List<DataModel> GetData()
        {
            List<DataModel> datas = new List<DataModel>();
            int end = count + 50;
            for (int i = count; i < end; i++)
            {
                datas.Add(new DataModel(i.ToString()));
            }
            count = end;

            return datas;
        }
    }
    public class DataModel
    {
        public DataModel(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
