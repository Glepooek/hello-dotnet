using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;

namespace Test.Loading
{
    public class StringToInlinesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            var text = value as string;
            if (text == null)
            {
                return null;
            }
            var words = GetWords(text);
            return GetInlines(words).ToList();
        }

        private static IEnumerable<Inline> GetInlines(IEnumerable<string> words)
        {
            return words.Select(x => new Run() { Text = $"{x} " });
        }

        private static IEnumerable<string> GetWords(string text)
        {
            return text.Split(' ');
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
