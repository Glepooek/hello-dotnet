using PluginManager;
using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookReader
{
    [Export(typeof(ITeachingComponent))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class Main : ITeachingComponent
    {
        private MainWindow _mainWindow;
        private bool _disposed;

        public void Dispose()
        {
            _disposed = true;
            _mainWindow?.Close();
        }

        public void Run(TeachingParams param, Action closeCallback)
        {
            _mainWindow = new MainWindow();
            _disposed = false;
            _mainWindow.Closed += (s, e) => 
            {
                if (_disposed)
                {
                    return;
                }
                closeCallback?.Invoke(); 
            };
            _mainWindow.Show();
        }
    }
}
