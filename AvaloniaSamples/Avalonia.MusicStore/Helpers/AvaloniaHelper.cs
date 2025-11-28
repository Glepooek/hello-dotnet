using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.Helpers
{
    public class AvaloniaHelper
    {
        public static Window? GetMainWindow()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desk)
            {
                return desk.MainWindow;
            }

            return null;
        }

        public static Window? GetTopLevel(Visual? visual)
        {
            return TopLevel.GetTopLevel(visual) as Window;
        }
    }
}
