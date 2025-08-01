﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface IWindowBridge
    {
        event EventHandler Loaded;
        event EventHandler SourceInitialized;
        event EventHandler Closed;
        IntPtr WindowHandle { get; }
    }

}
