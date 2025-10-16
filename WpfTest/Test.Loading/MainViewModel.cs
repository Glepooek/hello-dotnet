using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test.Loading
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string mCompanyName = "北京外研在线数字科技有限公司";
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return mCompanyName; }
            set
            {
                mCompanyName = value;
                RaisePropertyChanged(nameof(CompanyName));
            }
        }

        private double mFontSize = 10d;
        /// <summary>
        /// 字体大小
        /// </summary>
        public double FontSize
        {
            get { return mFontSize; }
            set
            {
                mFontSize = value;
                RaisePropertyChanged(nameof(FontSize));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
