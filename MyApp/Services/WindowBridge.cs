using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace MyApp.Services
{
    public class WindowBridgeService : IWindowBridge
    {
        private readonly Window _window;

        public event EventHandler Loaded;
        public event EventHandler SourceInitialized;

        public IntPtr WindowHandle => new WindowInteropHelper(_window).Handle;

        public WindowBridgeService(Window window)
        {
            _window = window;

            _window.Loaded += (_, __) => Loaded?.Invoke(this, EventArgs.Empty);
            _window.SourceInitialized += (_, __) => SourceInitialized?.Invoke(this, EventArgs.Empty);
        }
    }

}
