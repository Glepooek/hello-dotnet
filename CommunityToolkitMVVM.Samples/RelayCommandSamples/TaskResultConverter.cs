using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RelayCommandSamples
{
    public sealed class TaskResultConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Task<string> task)
            {
                // 在Result没有值之前，会阻塞 UI 线程，实际使用中请谨慎使用
                // 如何优化？在vm中使用异步方式获取结果，然后通过属性通知更新 UI
                return task.Result;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
